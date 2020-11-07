using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using coursework_itransition.Models;
using Identity.Models;

namespace coursework_itransition.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Composition>()
                .ToTable("Composition")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();
            builder.Entity<Chapter>()
                .ToTable("Chapter")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();
            base.OnModelCreating(builder);
        }

        public DbSet<Composition> Compositions { get; set; }

        public DbSet<Chapter> Chapters { get; set; }
    }
}
