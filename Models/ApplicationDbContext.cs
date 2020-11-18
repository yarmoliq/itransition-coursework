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
                .ToTable("Compositions")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();

            builder.Entity<Composition>()
                .HasOne(b => b.Author)
                .WithMany(b => b.Compositions)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Composition>()
                .HasMany(b => b.Chapters)
                .WithOne(b => b.Composition)
                .HasForeignKey(b => b.CompositionID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Composition>()
                .HasMany(b => b.Comments)
                .WithOne(b => b.Composition)
                .HasForeignKey(b => b.CompositionID)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Chapter>()
                .ToTable("Chapters")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();

            builder.Entity<Chapter>()
                .HasOne(b => b.Composition)
                .WithMany(b => b.Chapters)
                .HasForeignKey(b => b.CompositionID)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Entity<Comment>()
                .ToTable("Comments")
                .Property(e => e.ID)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }

        public DbSet<Composition> Compositions { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
