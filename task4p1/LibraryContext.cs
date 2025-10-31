using Microsoft.EntityFrameworkCore;
using task4p1.Models;

namespace task4p1.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(eb =>
            {
                eb.HasKey(a => a.Id);
                eb.Property(a => a.Name).HasMaxLength(200).IsRequired(false);
                eb.Property(a => a.DateOfBirth).IsRequired();
                eb.HasMany(a => a.Books)
                  .WithOne(b => b.Author!)
                  .HasForeignKey(b => b.AuthorId)
                  .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Book>(eb =>
            {
                eb.HasKey(b => b.Id);
                eb.Property(b => b.Title).HasMaxLength(400).IsRequired(false);
                eb.Property(b => b.PublishedYear).IsRequired();
                eb.Property(b => b.AuthorId).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}