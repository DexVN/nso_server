
using Nso.Core;
using Nso.Protocol;
using Nso.Server.Messaging;
using Nso.Server.Utils;

namespace Nso.Server.Handlers.NotLogin;

[CmdHandler(Cmd.NOT_LOGIN, (sbyte)NotLoginSub.SET_CLIENT_TYPE)]
public sealed class SetClientTypeHandler : ICommandHandler
{
    public async Task HandleAsync(ServerSession s, Message m)
    {
        // ── 1. Giải mã payload vào ClientInfo ─────────────────────
        using var br = new BinaryReader(new MemoryStream(m.Data, 1, m.Data.Length - 1));

        var info = s.ClientInfo;
        info.ClientType = br.ReadByte();
        info.ZoomLevel = br.ReadByte();
        info.IsGprs = br.ReadBoolean();

        info.ScreenWidth = br.ReadInt32BE();
        info.ScreenHeight = br.ReadInt32BE();

        info.IsQwerty = br.ReadBoolean();
        info.IsTouch = br.ReadBoolean();

        info.Platform = br.ReadUtfBE();
        info.VersionIp = br.ReadInt32BE();

        br.ReadByte();                     // placeholder 0
        info.LanguageId = br.ReadByte();
        info.UserProvider = br.ReadInt32BE();
        info.Agent = br.ReadUtfBE();

        Log.I($"[ClientInfo] type={info.ClientType}, zoom={info.ZoomLevel}, "
            + $"{info.ScreenWidth}×{info.ScreenHeight}, platform='{info.Platform}', agent='{info.Agent}'");

        // ── 2. Gửi ACK cho client ────────────────────────────────
        // payload = [subCmd (-125)][status 1=OK]
        var ackPayload = new byte[]
        {
            unchecked((byte)NotLoginSub.SET_CLIENT_TYPE),   // -125
            1                                               // OK
        };

        await s.SendAsync(new Message((sbyte)Cmd.NOT_LOGIN, ackPayload));

        // Không cần await gì thêm; handler xong.
    }
}
