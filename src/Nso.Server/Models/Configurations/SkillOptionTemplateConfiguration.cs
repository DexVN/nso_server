using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class SkillOptionTemplateConfiguration : IEntityTypeConfiguration<SkillOptionTemplate>
{
    public void Configure(EntityTypeBuilder<SkillOptionTemplate> builder)
    {
        builder.ToTable("SkillOptionTemplates");
        builder.HasKey(sot => sot.Id);
        builder.Property(sot => sot.Name).IsRequired().HasMaxLength(100);
    }
}
