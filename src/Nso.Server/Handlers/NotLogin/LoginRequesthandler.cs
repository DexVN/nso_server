using System.Text;

using Microsoft.EntityFrameworkCore;

using Nso.Core;
using Nso.Protocol;
using Nso.Server.Messaging;
using Nso.Server.Models;
using Nso.Server.Utils;

namespace Nso.Server.Handlers.NotLogin;

[CmdHandler(Cmd.NOT_LOGIN, (sbyte)NotLoginSub.LOGIN_REQUEST)]
public sealed class LoginRequestHandler : ICommandHandler
{
    private readonly AppDbContext _context;

    public LoginRequestHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task HandleAsync(ServerSession s, Message m)
    {
        using var br = new BinaryReader(new MemoryStream(m.Data, 1, m.Data.Length - 1));
        string user = br.ReadUtfBE();
        string pass = br.ReadUtfBE();
        _ = br.ReadUtfBE(); // version
        _ = br.ReadUtfBE(); // reserved1
        _ = br.ReadUtfBE(); // reserved2
        _ = br.ReadUtfBE(); // randomStr
        _ = br.ReadByte();  // serverLogin

        s.IsAuthenticated = true;

        using var msLogin = new MemoryStream();
        var w = new BinaryWriter(msLogin);
        w.Write((sbyte)NotLoginSub.LOGIN_REQUEST);
        w.Write((sbyte)1);
        w.WriteUtfBE("OK");
        await s.SendAsync(new Message((sbyte)Cmd.NOT_LOGIN, msLogin.ToArray()));

        await s.SendAsync(new Message((sbyte)Cmd.info_kiemduyet, new byte[] { 2 }));

        var versionDict = await _context.DataVersions.AsNoTracking()
            .ToDictionaryAsync(x => x.Key, x => x.Version);

        byte[] verPayload = {
            (byte)(versionDict.GetValueOrDefault("Data")),
            (byte)(versionDict.GetValueOrDefault("Map")),
            (byte)(versionDict.GetValueOrDefault("Skill")),
            (byte)(versionDict.GetValueOrDefault("Item"))
        };
        await s.SendAsync(Cmd.NOT_MAP, sub: -123, extra: verPayload);

        // ===== [Skill Payload - createSkill()] =====
        byte vcSkill = (byte)(versionDict.GetValueOrDefault("Skill"));

        var optionTemplates = await _context.SkillOptionTemplates
            .OrderBy(t => t.Id)
            .AsNoTracking()
            .ToListAsync();

        var classes = await _context.NClasses
            .Include(c => c.SkillTemplates)
                .ThenInclude(st => st.Skills)
                    .ThenInclude(sk => sk.SkillOptions)
                        .ThenInclude(opt => opt.SkillOptionTemplate)
            .OrderBy(c => c.Id)
            .AsNoTracking()
            .ToListAsync();

        using var ms = new MemoryStream();
        var bw = new BinaryWriter(ms, Encoding.UTF8);

        // [1] vcSkill
        bw.Write(vcSkill);

        // [2] OptionTemplates
        bw.Write((byte)optionTemplates.Count);
        foreach (var opt in optionTemplates)
            bw.WriteUtfBE(opt.Name);

        // [3] NClass list
        bw.Write((byte)classes.Count);
        foreach (var cls in classes)
        {
            bw.WriteUtfBE(cls.Name);
            bw.Write((byte)cls.SkillTemplates.Count);

            foreach (var st in cls.SkillTemplates.OrderBy(st => st.Id))
            {
                bw.Write((byte)st.Id);
                bw.WriteUtfBE(st.Name);
                bw.Write((byte)st.MaxPoint);
                bw.Write((byte)st.Type);
                bw.WriteUInt16BE((ushort)st.IconId); // short

                bw.WriteUtfBE((st.Description ?? "").Replace("\n", " ").Replace("\r", ""));

                bw.Write((byte)st.Skills.Count);
                foreach (var sk in st.Skills.OrderBy(sk => sk.Id))
                {
                    bw.WriteUInt16BE((ushort)sk.Id); // short
                    bw.Write((byte)sk.Point);
                    bw.Write((byte)sk.Level);
                    bw.WriteUInt16BE((ushort)sk.ManaUse); // short
                    bw.WriteInt32BE(sk.CoolDown); // int (big-endian)
                    bw.WriteUInt16BE((ushort)sk.Dx);
                    bw.WriteUInt16BE((ushort)sk.Dy);
                    bw.Write((byte)sk.MaxFight);

                    bw.Write((byte)sk.SkillOptions.Count);
                    foreach (var so in sk.SkillOptions)
                    {
                        bw.WriteUInt16BE((ushort)so.Param); // short
                        bw.Write((byte)so.SkillOptionTemplateId); // byte id
                    }
                }
            }
        }
        Log.I($"[Skill Payload] Classes: {classes.Count}, Options: {optionTemplates.Count}");
        await s.SendAsync(Cmd.NOT_MAP, sub: -120, extra: ms.ToArray());

        // ===== [Account] =====
        var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Username == user && a.Password == pass);

        if (account != null)
        {
            var characters = await _context.Characters
                .Where(c => c.AccountId == account.Id)
                .OrderBy(c => c.Id)
                .AsNoTracking()
                .ToListAsync();

            using var msChar = new MemoryStream();
            var bwChar = new BinaryWriter(msChar, Encoding.UTF8);

            // [0] số lượng nhân vật
            bwChar.Write((byte)characters.Count);

            foreach (var ch in characters)
            {
                bwChar.Write((byte)ch.Gender);
                bwChar.WriteUtfBE(ch.Name);
                bwChar.WriteUtfBE(ch.ClassName);
                bwChar.Write((byte)ch.Level);
                bwChar.WriteUInt16BE((ushort)ch.PartHead);
                bwChar.WriteUInt16BE((ushort)ch.PartWeapon);
                bwChar.WriteUInt16BE((ushort)ch.PartBody);
                bwChar.WriteUInt16BE((ushort)ch.PartLeg);
            }

            Log.I($"[Login] Sent character list for account '{user}', count: {characters.Count}");
            await s.SendAsync(Cmd.NOT_MAP, sub: -126, extra: msChar.ToArray());
        }
        else
        {
            Log.W($"[Login] Invalid credentials for user '{user}'");
            // Optional: send back error message if needed
        }
    }
}
