using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .Property(p => p.Id)
                .IsRequired();

            builder
                .Property(p => p.CustomerId)
                .IsRequired();

            builder
                .Property(p => p.Quantity)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .IsRequired();

            builder
                .Property(p => p.Status)
                .IsRequired();

            builder.OwnsOne(a => a.Address, a =>
            {
                a.Property(p => p.AddressLine).IsRequired();
                a.Property(p => p.City).IsRequired();
                a.Property(p => p.Country).IsRequired();
                a.Property(p => p.CityCode).IsRequired();
            });

            builder.OwnsOne(a => a.Product, a =>
            {
                a.Property(p => p.Id).IsRequired();
                a.Property(p => p.Name).IsRequired();
                a.Property(p => p.ImageUrl).IsRequired();
            });

            builder
                .Property(p => p.CreatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            builder
                .Property(p => p.UpdatedAt)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();
        }
    }


}
