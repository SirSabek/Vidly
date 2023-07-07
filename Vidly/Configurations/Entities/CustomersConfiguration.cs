using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Models;

namespace Vidly.Configurations.Entities;

public class CustomersConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasData(
            new Customer
            {
                Id = 1,
                Name = "John Smith",
                IsSubscribedToNewsLetter = true,
                MembershipTypeId = 1,
                BirthDate = new DateTime(1990, 01, 01)
            },
            new Customer
            {
                Id = 2,
                Name = "Mary Williams",
                IsSubscribedToNewsLetter = false,
                MembershipTypeId = 2,
                BirthDate = new DateTime(1995, 01, 01)
            },
            new Customer
            {
                Id = 3,
                Name = "John Doe",
                IsSubscribedToNewsLetter = true,
                MembershipTypeId = 3,
                BirthDate = new DateTime(2000, 01, 01)
            },
            new Customer
            {
                Id = 4,
                Name = "Jane Doe",
                IsSubscribedToNewsLetter = false,
                MembershipTypeId = 4,
                BirthDate = new DateTime(2005, 01, 01)
            });
    }
}