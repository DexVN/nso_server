using Nso.Server.Models;

namespace Nso.Server.Seed;

public static class SeedAccount
{
    public static void Run(AppDbContext context)
    {
        // Kiểm tra xem đã có tài khoản nào chưa
        if (context.Accounts.Any())
        {
            Console.WriteLine("[SEED] ✅ Accounts already exist — skipping.");
            return;
        }

        // Tạo tài khoản mặc định
        var account = new Account
        {
            Username = "admin",
            Password = "123456", // Lưu ý: dùng hashed password nếu thật
            Email = "admin@example.com",
            Characters = new List<Character>
            {
                new Character
                {
                    Name = "ga122",
                    Gender = 0,
                    ClassName = "Chưa vào lớp",
                    Level = 1,
                    PartHead = 27,
                    PartWeapon = 15,
                    PartBody = 10,
                    PartLeg = 9,
                    SlotIndex = 0
                },
                new Character
                {
                    Name = "updhdx3223",
                    Gender = 0,
                    ClassName = "Ninja Quạt",
                    Level = 98,
                    PartHead = 28,
                    PartWeapon = 16,
                    PartBody = 10,
                    PartLeg = 142,
                    SlotIndex = 1
                }
            }
        };

        context.Accounts.Add(account);
        context.SaveChanges();

        Console.WriteLine("[SEED] ✅ Seeded default account with 2 characters.");
    }
}
