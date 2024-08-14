using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Models;

namespace Vidly.Configurations.Entities;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasData(
            new Genre
            {
                Id = 1,
                Name = "Action"
            },
            new Genre
            {
                Id = 2,
                Name = "Comedy"
            },
            new Genre
            {
                Id = 3,
                Name = "Horror"
            },
            new Genre
            {
                Id = 4,
                Name = "Tragedy"
            }
        );
    }
}