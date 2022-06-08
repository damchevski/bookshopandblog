using BSB.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BSB.Data
{
    public class ApplicationDbContext : IdentityDbContext<MAUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<FavouriteMovies> FavouriteMovies { get; set; }
        public virtual DbSet<UserFavMovie> UserFavMovie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<FavouriteMovies>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<UserFavMovie>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.FavOnUsers)
                .HasForeignKey(z => z.FavMoviesId);

            builder.Entity<UserFavMovie>()
                .HasOne(z => z.FavMovies)
                .WithMany(z => z.MovieInUserFavourites)
                .HasForeignKey(z => z.MovieId);


            builder.Entity<FavouriteMovies>()
                .HasOne<MAUser>(z => z.User)
                .WithOne(z => z.UserFavouriteMovies)
                .HasForeignKey<FavouriteMovies>(z => z.UserId);

        }
    }
}

