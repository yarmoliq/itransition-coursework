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
        {Add table chapter to database and connection of tables

add : connection user to composition
add : connection composition to chapter
change : models chapter, composition, user
add : view author name in index
add : view all chapters jf composition in index
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
