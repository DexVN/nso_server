using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");
        builder.HasKey(x => x.Id);

        builder.HasMany(s => s.SkillOptions)
               .WithOne(so => so.Skill)
               .HasForeignKey(o => o.SkillId);
    }
}
