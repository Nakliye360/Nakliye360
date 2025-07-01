using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nakliye360.Domain.Entities.CustomerManagement;

namespace Nakliye360.Persistence.Configurations.CustomerManagement;


public class CorporateCustomerConfig : IEntityTypeConfiguration<CorporateCustomer>
{
    public void Configure(EntityTypeBuilder<CorporateCustomer> builder)
    {
        builder.ToTable("CorporateCustomers");

        builder.HasKey(cc => cc.Id);

        builder.Property(cc => cc.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(cc => cc.TaxNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasOne(cc => cc.Customer)
            .WithOne(c=> c.CorporateCustomer)
            .HasForeignKey<CorporateCustomer>(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}