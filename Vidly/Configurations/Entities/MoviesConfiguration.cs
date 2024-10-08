﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vidly.Models;

namespace Vidly.Configurations.Entities;

public class MoviesConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasData(
            new Movie
            {
                Id = 1,
                Name = "Shrek",
                GenreId = 1,
                ReleaseDate = new DateTime(2001, 6, 22),
                DateAdded = new DateTime(2021, 8, 1),
                NumberInStock = 6
            },
            new Movie
            {
                Id = 2,
                Name = "Wall-e",
                GenreId = 2,
                ReleaseDate = new DateTime(2008, 6, 27),
                DateAdded = new DateTime(2021, 8, 1),
                NumberInStock = 3
            },
            new Movie
            {
                Id = 3,
                Name = "The Hangover",
                GenreId =3,
                ReleaseDate = new DateTime(2009, 6, 5),
                DateAdded = new DateTime(2021, 8, 1),
                NumberInStock = 5
            },
            new Movie
            {
                Id = 4,
                Name = "Anastasia",
                GenreId =3,
                ReleaseDate = new DateTime(1997, 11, 21),
                DateAdded = new DateTime(2021, 8, 1),
                NumberInStock = 2
            },
            new Movie
            {
                Id = 5,
                Name = "Die Hard",
                GenreId = 1,
                ReleaseDate = new DateTime(1988, 7, 15),
                DateAdded = new DateTime(2021, 8, 1),
                NumberInStock = 4
            });
    }
}