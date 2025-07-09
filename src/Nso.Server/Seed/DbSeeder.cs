using Nso.Server.Models;
using Nso.Server.Seed;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        SeedDataVersion.Run(context);
        SeedSkillOptionTemplate.Run(context);
        SeedNClass.Run(context);
        SeedAccount.Run(context);

        context.SaveChanges();
    }
}
