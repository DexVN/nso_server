using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using Nso.Server.Models;

namespace Nso.Server.Seed;

public static class SeedSkillOptionTemplate
{
    public static void Run(AppDbContext context)
    {
        var path = Path.Combine("Data", "SkillOptionTemplate.json");
        if (!File.Exists(path))
        {
            Console.WriteLine($"[SEED] ⚠ File not found: {path}");
            return;
        }

        // Đọc JSON (chỉ dùng nếu cần)
        var json = File.ReadAllText(path);
        var root = JsonDocument.Parse(json).RootElement;

        var jsonOptions = root.GetProperty("SkillOptionTemplate")
            .EnumerateArray()
            .Select(el => new SkillOptionTemplate
            {
                Id = el.GetProperty("Id").GetInt32(),
                Name = el.GetProperty("Name").GetString()!
            })
            .ToList();

        var existingIds = context.SkillOptionTemplates
            .AsNoTracking()
            .Select(x => x.Id)
            .ToHashSet();

        // Nếu đã có đủ ID → bỏ qua hoàn toàn
        if (jsonOptions.All(opt => existingIds.Contains(opt.Id)))
        {
            Console.WriteLine("[SEED] ✅ SkillOptionTemplates already complete — skipping.");
            return;
        }

        // Chỉ thêm những cái còn thiếu
        var newOptions = jsonOptions
            .Where(opt => !existingIds.Contains(opt.Id))
            .ToList();

        if (newOptions.Any())
        {
            context.SkillOptionTemplates.AddRange(newOptions);
            context.SaveChanges();
            Console.WriteLine($"[SEED] ✅ Seeded {newOptions.Count} SkillOptionTemplates");
        }
    }
}
