using Microsoft.EntityFrameworkCore;

namespace Nso.Server.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<NClass> NClasses { get; set; }
    public DbSet<SkillTemplate> SkillTemplates { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<SkillOption> SkillOptions { get; set; }
    public DbSet<SkillOptionTemplate> SkillOptionTemplates { get; set; }
    public DbSet<DataVersion> DataVersions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
