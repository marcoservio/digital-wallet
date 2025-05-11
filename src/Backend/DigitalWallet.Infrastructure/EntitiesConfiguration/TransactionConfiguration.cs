namespace DigitalWallet.Infrastructure.EntitiesConfiguration;

public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        builder.Property(t => t.Description).HasMaxLength(255).IsRequired();
        builder.Property(t => t.Status).IsRequired();

        builder.HasData(
            new Transaction { Id = 1, FromWalletId = 1, ToWalletId = 2, Amount = 100, Description = "Admin to Bob", CreatedAt = DateTime.UtcNow },
            new Transaction { Id = 2, FromWalletId = 2, ToWalletId = 3, Amount = 50, Description = "Bob to Charlie", CreatedAt = DateTime.UtcNow },
            new Transaction { Id = 3, FromWalletId = 3, ToWalletId = 1, Amount = 30, Description = "Charlie to Admin", CreatedAt = DateTime.UtcNow });
    }
}
