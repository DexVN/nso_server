using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Nso.Server.Models.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Username)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(c => c.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany(c => c.Characters)
            .WithOne(c => c.Account)
            .HasForeignKey(c => c.AccountId);
    }
}

