using BookOrgWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOrgWebapp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(b => new { b.ISBN, b.UserID });

        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.Title)
            .WithMany(t => t.Books)
            .HasForeignKey(b => b.ISBN)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserID)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleId)
            .IsUnique()
            .HasFilter("[GoogleId] IS NOT NULL");

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // Dummy data

        modelBuilder
            .Entity<Title>()
            .HasData(
                new Title
                {
                    ISBN = "9780008536695",
                    BookName = "How to Kill Men and Get Away With It",
                },
                new Title
                {
                    ISBN = "9780425261019",
                    BookName = "Let's Pretend This Never Happened",
                },
                new Title
                {
                    ISBN = "9798217189052",
                    BookName = "I'm Not the Only Murderer in My Retirement Home",
                },
                new Title
                {
                    ISBN = "9781250359643",
                    BookName = "99 Ways to Die: And How to Avoid Them",
                }
            );

        modelBuilder
            .Entity<User>()
            .HasData(
                new User
                {
                    UserID = 001,
                    Name = "Annika",
                    Email = "Jannika@gmail.com",
                },
                new User
                {
                    UserID = 002,
                    Name = "Clara",
                    Email = "Clara@gmail.com",
                },
                new User
                {
                    UserID = 003,
                    Name = "Anna",
                    Email = "Anna@gmail.com",
                }
            );

        modelBuilder
            .Entity<Book>()
            .HasData(
                new Book { ISBN = "9780008536695", UserID = 001 },
                new Book { ISBN = "9780425261019", UserID = 001 },
                new Book { ISBN = "9798217189052", UserID = 003 },
                new Book { ISBN = "9780425261019", UserID = 003 },
                new Book { ISBN = "9780425261019", UserID = 002 },
                new Book { ISBN = "9781250359643", UserID = 002 }
            );
    }
}
