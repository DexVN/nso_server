using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class SkillOptionsConfiguration : IEntityTypeConfiguration<SkillOption>
{
    public void Configure(EntityTypeBuilder<SkillOption> builder)
    {
        builder.ToTable("SkillOptions");

        // Dùng composite key
        builder.HasKey(so => new { so.SkillId, so.SkillOptionTemplateId });

        builder.HasOne(so => so.Skill)
               .WithMany(s => s.SkillOptions)
               .HasForeignKey(so => so.SkillId);

        builder.HasOne(so => so.SkillOptionTemplate)
               .WithMany()
               .HasForeignKey(so => so.SkillOptionTemplateId);

    }
}
