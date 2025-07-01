using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nakliye360.Domain.Entities.CustomerManagement;

namespace Nakliye360.Persistence.Configurations.CustomerManagement;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.IdentityNumber).IsUnique();

        builder.Property(c => c.IdentityNumber)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(c => c.Email)
            .HasMaxLength(100);

        builder.Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.CustomerType)
            .IsRequired();

      


        builder.HasOne(c => c.AppUser)
            .WithMany() // ❗ çünkü bir kullanıcı (AppUser) birden fazla Customer olabilir
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.IndividualCustomer)
            .WithOne(ic => ic.Customer)
            .HasForeignKey<IndividualCustomer>(ic => ic.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.CorporateCustomer)
            .WithOne(cc => cc.Customer)
            .HasForeignKey<CorporateCustomer>(cc => cc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.ProducerCustomer)
            .WithOne(pc => pc.Customer)
            .HasForeignKey<ProducerCustomer>(pc => pc.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}