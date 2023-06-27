using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Models;

namespace Vidly.Configurations.Entities;

public class MembershipConfiguration : IEntityTypeConfiguration<MembershipType>
{
    public void Configure(EntityTypeBuilder<MembershipType> builder)
    {
        builder.HasData(
            new MembershipType
            {
                Id = 1,
                SignUpFee = 0,
                DurationInMonths = 0,
                DiscountRate = 0
            },
            new MembershipType
            {
                Id = 2,
                SignUpFee = 30,
                DurationInMonths = 1,
                DiscountRate = 10
            },
            new MembershipType
            {
                Id = 3,
                SignUpFee = 90,
                DurationInMonths = 3,
                DiscountRate = 15
            },
            new MembershipType
            {
                Id = 4,
                SignUpFee = 300,
                DurationInMonths = 12,
                DiscountRate = 20
            }
        );
    }
}