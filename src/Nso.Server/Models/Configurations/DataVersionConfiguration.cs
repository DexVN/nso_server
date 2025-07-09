

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Nso.Server.Models.Configurations;

public class DataVersionConfiguration : IEntityTypeConfiguration<DataVersion>
{
    public void Configure(EntityTypeBuilder<DataVersion> builder)
    {
        builder.ToTable("DataVersions");
        builder.HasKey(dt => dt.Key);
    }
}
