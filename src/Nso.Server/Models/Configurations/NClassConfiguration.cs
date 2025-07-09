using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class NClassConfiguration : IEntityTypeConfiguration<NClass>
{
    public void Configure(EntityTypeBuilder<NClass> builder)
    {
        builder.ToTable("NClasses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasMany(c => c.SkillTemplates)
               .WithOne(t => t.NClass)
               .HasForeignKey(t => t.NClassId);
    }
}
