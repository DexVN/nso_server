using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using Nso.Core;
using Nso.Server.Models;

namespace Nso.Server.Seed;

public static class SeedNClass
{
    public static void Run(AppDbContext context)
    {
        if (context.NClasses.Any())
        {
            Log.E("[SEED] ✅ NClasses already seeded — skipping.");
            return;
        }

        var json = File.ReadAllText("Data/NClass.json");
        var root = JsonSerializer.Deserialize<Dictionary<string, List<NClassSeed>>>(json);
        if (root == null || !root.TryGetValue("NClass", out var data))
        {
            Log.E("❌ Failed to deserialize JSON or 'NClass' key is missing.");
            return;
        }

        // Load sẵn SkillOptionTemplate vào cache
        var optionTemplateCache = context.SkillOptionTemplates
            .AsNoTracking()
            .ToDictionary(x => x.Id);

        foreach (var nc in data)
        {
            Log.E($"[SEED] {nc.Name}");

            var nclass = new NClass
            {
                Id = nc.ClassId,
                Name = nc.Name,
                SkillTemplates = nc.SkillTemplate.Select(st => new SkillTemplate
                {
                    Id = st.Id,
                    Name = st.Name,
                    MaxPoint = st.MaxPoint,
                    Type = st.Type,
                    IconId = st.IconId,
                    Description = st.Description,
                    Skills = st.Skill.Select(s =>
                    {
                        var skill = new Skill
                        {
                            Id = s.SkillId,
                            Point = s.Point,
                            Level = s.Level,
                            ManaUse = s.ManaUse,
                            CoolDown = s.CoolDown,
                            Dx = s.Dx,
                            Dy = s.Dy,
                            MaxFight = s.MaxFight,
                        };

                        skill.SkillOptions = s.Options
                            .Where(opt => optionTemplateCache.ContainsKey(opt.OptionTemplate.Id))
                            .Select(opt => new SkillOption
                            {
                                SkillId = s.SkillId, // 👈 BẮT BUỘC vì là composite key
                                SkillOptionTemplateId = opt.OptionTemplate.Id,
                                Param = opt.Param
                            }).ToList();

                        return skill;
                    }).ToList()
                }).ToList()
            };

            context.NClasses.Add(nclass);
            context.SaveChanges();
        }
    }
}
