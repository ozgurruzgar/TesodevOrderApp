using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(p => p.Id)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.Email)
                .IsRequired();

            builder.OwnsOne(a => a.Address, a =>
            {
                a.Property(p => p.AddressLine).IsRequired();
                a.Property(p => p.City).IsRequired();
                a.Property(p => p.Country).IsRequired();
                a.Property(p => p.CityCode).IsRequired();
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
