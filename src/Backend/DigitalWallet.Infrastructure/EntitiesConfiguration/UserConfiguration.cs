namespace DigitalWallet.Infrastructure.EntitiesConfiguration;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder
            .Property(c => c.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(c => c.Password)
            .HasMaxLength(2000)
            .IsRequired();

        builder
           .Property(w => w.UserIdentifier)
           .IsRequired();

        builder
            .HasIndex(w => w.UserIdentifier)
            .IsUnique();

        builder
            .HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(w => w.UserId);

        builder.HasData(
        new User { Id = 1, Name = "Admin", Email = "admin@gmail.com", Password = new Security.Cryptography.BCryptNet().Encrypt("root") },
        new User { Id = 2, Name = "Bob", Email = "bob@gmail.com", Password = new Security.Cryptography.BCryptNet().Encrypt("bob123") },
        new User { Id = 3, Name = "Charlie", Email = "charlie@gmail.com", Password = new Security.Cryptography.BCryptNet().Encrypt("charlie123") });
    }
}
