using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MovieManagement.Models
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//calling onmodelcreating for identityclass else u will get migrationerror
            modelBuilder.Entity<Movie>().HasData(
                new Movie() { ID = 1, Price = 23, Genre = Generes.Action, Title = "Star Wars", Rating = 3, ReleaseDate = DateTime.Today }
                );
        }
    }
}
