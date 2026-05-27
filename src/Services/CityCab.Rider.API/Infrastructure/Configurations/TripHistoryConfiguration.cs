namespace CityCab.Rider.API.Infrastructure.Configurations
{
    public class TripHistoryConfiguration : IEntityTypeConfiguration<TripHistory>
    {
        public void Configure(EntityTypeBuilder<TripHistory> builder)
        {
            builder.ToTable("TripHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TripId)
                .IsRequired();

            builder.Property(x => x.RiderId)
                .IsRequired();

            builder.Property(x => x.DriverId)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion<string>() // optional: stores enum as string
                .HasMaxLength(50);

            builder.Property(x => x.PickupLocation)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.DropoffLocation)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.StartedAt)
                .IsRequired();

            builder.Property(x => x.CompletedAt)
                .IsRequired(false);

            builder.Property(x => x.IsActive)
                .IsRequired();

            // assuming BaseTimeStampedModel has these
            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.HasIndex(x => x.TripId)
                .IsUnique();

            builder.HasIndex(x => x.RiderId);

            builder.HasIndex(x => x.DriverId);

            builder.HasIndex(x => x.Status);

            builder.HasIndex(x => x.IsActive);
        }
    }
}
