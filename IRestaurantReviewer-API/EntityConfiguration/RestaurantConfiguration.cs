using IRestaurantReviewer_API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IRestaurantReviewer_API.EntityConfiguration
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.City)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(i => i.PostCode)
              .IsRequired()
              .HasMaxLength(50);

            builder.HasMany(x => x.Photos)
                .WithOne(x => x.Restaurant)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
