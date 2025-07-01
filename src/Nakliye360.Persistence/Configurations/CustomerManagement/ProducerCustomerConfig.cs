using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nakliye360.Domain.Entities.CustomerManagement;

namespace Nakliye360.Persistence.Configurations.CustomerManagement;

public class ProducerCustomerConfig : IEntityTypeConfiguration<ProducerCustomer>
{
    public void Configure(EntityTypeBuilder<ProducerCustomer> builder)
    {
        builder.ToTable("ProducerCustomers");

        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.ProductionType)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(pc => pc.CompanyName)
            .HasMaxLength(200);

        builder.HasOne(pc => pc.Customer)
            .WithOne(c=> c.ProducerCustomer)
            .HasForeignKey<ProducerCustomer>(pc => pc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}