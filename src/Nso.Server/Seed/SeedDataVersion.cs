using Nso.Server.Models;

namespace Nso.Server.Seed;

public static class SeedDataVersion
{
    public static void Run(AppDbContext context)
    {
        var requiredKeys = new[] { "Data", "Skill", "Item", "Map" };

        // 👉 Kiểm tra nếu tất cả keys đã tồn tại
        var existingKeys = context.DataVersions
            .Select(x => x.Key)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (requiredKeys.All(k => existingKeys.Contains(k)))
        {
            Console.WriteLine("[SEED] ✅ DataVersions already complete — skipping.");
            return;
        }

        // Thêm các key còn thiếu
        foreach (var key in requiredKeys)
        {
            if (!existingKeys.Contains(key))
            {
                context.DataVersions.Add(new DataVersion
                {
                    Key = key,
                    Version = 1,
                    UpdatedAt = DateTime.UtcNow
                });
                Console.WriteLine($"[SEED] + Added missing version: {key}");
            }
        }

        context.SaveChanges();
        Console.WriteLine("[SEED] ✅ DataVersions seeded (partial update)");
    }
}
