using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class SkillTemplateConfiguration : IEntityTypeConfiguration<SkillTemplate>
{
    public void Configure(EntityTypeBuilder<SkillTemplate> builder)
    {
        builder.ToTable("SkillTemplates");
        builder.HasKey(st => st.Id);
        builder.Property(st => st.Name).IsRequired().HasMaxLength(100);

        builder.HasMany(st => st.Skills)
               .WithOne(s => s.SkillTemplate)
               .HasForeignKey(s => s.SkillTemplateId);
    }
}
