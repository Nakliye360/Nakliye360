using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nakliye360.Domain.Entities.CustomerManagement;

namespace Nakliye360.Persistence.Configurations.CustomerManagement;

public class IndividualCustomerConfig : IEntityTypeConfiguration<IndividualCustomer>
{
    public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
    {
        builder.ToTable("IndividualCustomers");

        builder.HasKey(ic => ic.Id);

        builder.Property(ic => ic.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(ic => ic.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(ic => ic.Customer)
           .WithOne(c => c.IndividualCustomer)
           .HasForeignKey<IndividualCustomer>(ic => ic.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
