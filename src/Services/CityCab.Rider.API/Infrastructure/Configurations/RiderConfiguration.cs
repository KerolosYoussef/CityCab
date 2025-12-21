namespace CityCab.Rider.API.Infrastructure.Configurations
{
    public class RiderConfiguration : IEntityTypeConfiguration<Models.Rider>
    {
        public void Configure(EntityTypeBuilder<Models.Rider> builder)
        {
            builder.ToTable("Riders");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.PhoneNumber)
                .IsRequired()
                .HasMaxLength(13);

            builder.Property(r => r.Rating)
                .HasPrecision(3,2)
                .HasDefaultValue(5.0m);

            builder.Navigation(r => r.Addresses)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(r => r.PaymentMethods)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(r => r.Addresses, a =>
            {
                a.ToTable("RiderAddresses");
                a.WithOwner().HasForeignKey(a => a.RiderId);
                a.HasKey(a => new {a.Id, a.RiderId});
            });

            builder.OwnsMany(r => r.PaymentMethods, a =>
            {
                a.ToTable("RiderPaymentMethods");
                a.WithOwner().HasForeignKey(a => a.RiderId);
                a.HasKey(a => new { a.Id, a.RiderId });
            });

            builder.HasIndex(r => r.Email)
                .IsUnique();
                
        }
    }
}
