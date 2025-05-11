namespace DigitalWallet.Infrastructure.EntitiesConfiguration;

public class WalletConfiguration : BaseEntityConfiguration<Wallet>
{
    public override void Configure(EntityTypeBuilder<Wallet> builder)
    {
        base.Configure(builder);

        builder
           .Property(w => w.Balance)
           .HasPrecision(18, 2);

        builder
            .Property(w => w.WalletKey)
            .IsRequired();

        builder
            .HasIndex(w => w.WalletKey)
            .IsUnique();

        builder
            .Property(w => w.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        builder
            .HasMany(w => w.SentTransactions)
            .WithOne(t => t.FromWallet)
            .HasForeignKey(t => t.FromWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(w => w.ReceivedTransactions)
            .WithOne(t => t.ToWallet)
            .HasForeignKey(t => t.ToWalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
           new Wallet { Id = 1, UserId = 1, Balance = 1000 },
           new Wallet { Id = 2, UserId = 2, Balance = 500 },
           new Wallet { Id = 3, UserId = 3, Balance = 300 });
    }
}
