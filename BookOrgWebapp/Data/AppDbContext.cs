using BookOrgWebapp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOrgWebapp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<UserBook> UserBooks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBook>().HasKey(b => new { b.ISBN, b.UserID });

        modelBuilder
            .Entity<BookAuthor>()
            .HasOne(ba => ba.Title)
            .WithMany(t => t.BookAuthors)
            .HasForeignKey(ba => ba.ISBN);

        modelBuilder
            .Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorID);

        modelBuilder
            .Entity<BookGenre>()
            .HasOne(bg => bg.Title)
            .WithMany(t => t.BookGenres)
            .HasForeignKey(bg => bg.ISBN);

        modelBuilder
            .Entity<UserBook>()
            .HasOne(b => b.Title)
            .WithMany(t => t.UserBooks)
            .HasForeignKey(b => b.ISBN)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<UserBook>()
            .HasOne(b => b.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(b => b.UserID)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasIndex(u => u.GoogleId)
            .IsUnique()
            .HasFilter("[GoogleId] IS NOT NULL");

        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Author>().HasIndex(a => a.AuthorName).IsUnique();

        modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.ISBN, ba.AuthorID });

        modelBuilder.Entity<BookGenre>().HasKey(bg => new { bg.ISBN, bg.Genre });

        // Dummy data

        modelBuilder
            .Entity<Title>()
            .HasData(
                new Title
                {
                    ISBN = "9780008536695",
                    BookName = "How to Kill Men and Get Away With It",
                    Description = "Meet Kitty Collins. FRIEND. LOVER. KILLER.",
                    Publisher = "HQ Digital",
                    PublishedDate = new DateTime(2023, 2, 16),
                    Language = "en",
                    PageCount = 0,
                    ISBN10 = "0008536694",
                    ThumbnailSmall =
                        "http://books.google.com/books/content?id=3XZSzwEACAAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api",
                    ThumbnailNormal =
                        "http://books.google.com/books/content?id=3XZSzwEACAAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api",
                },
                new Title
                {
                    ISBN = "9780425261019",
                    BookName = "Let's Pretend This Never Happened",
                    SubTitle = "A Mostly True Memoir",
                    Description =
                        "The #1 New York Times bestselling (mostly true) memoir from the hilarious author of Furiously Happy. “Gaspingly funny and wonderfully inappropriate.”—O, The Oprah Magazine When Jenny Lawson was little, all she ever wanted was to fit in. That dream was cut short by her fantastically unbalanced father and a morbidly eccentric childhood. It did, however, open up an opportunity for Lawson to find the humor in the strange shame-spiral that is her life, and we are all the better for it. In the irreverent Let’s Pretend This Never Happened, Lawson’s long-suffering husband and sweet daughter help her uncover the surprising discovery that the most terribly human moments—the ones we want to pretend never happened—are the very same moments that make us the people we are today. For every intellectual misfit who thought they were the only ones to think the things that Lawson dares to say out loud, this is a poignant and hysterical look at the dark, disturbing, yet wonderful moments of our lives. Readers Guide Inside",
                    Publisher = "Penguin",
                    PublishedDate = new DateTime(2013, 3, 5),
                    Language = "en",
                    PageCount = 385,
                    ISBN10 = "0425261018",
                    ThumbnailNormal =
                        "http://books.google.com/books/content?id=XVSLDQAAQBAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api",
                    ThumbnailSmall =
                        "http://books.google.com/books/content?id=XVSLDQAAQBAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api",
                },
                new Title
                {
                    ISBN = "9798217189052",
                    BookName = "I'm Not the Only Murderer in My Retirement Home",
                    Description =
                        "After a decades-long stint in prison, former serial killer Carol is looking to kick back and relax in her new retirement home...until a fellow resident drops dead and Carol has to prove she actually didn't do it this time.... Carol is delighted to be leaving her tiny prison cell behind to take her place in a luxury retirement home. She's hoping her past as a serial killer won't come to light so she can make a few friends and find some murder-free hobbies. But it's not long before a fellow resident—who happens to be a former police commissioner—drops dead, and Carol's true identity is leaked—making catching up over daily activities of bingo and baking rather awkward. Just her luck, Carol soon realizes that the victim wasn't the only former law enforcement officer at Sheldon Oaks—it's filled to the brim with former cops, barristers, and government representatives, her newfound friends included. And everyone thinks Carol's guilt is a no-brainer, but she is ready to prove them dead wrong...without killing anyone, for once.",
                    Publisher = "Penguin Group",
                    PublishedDate = new DateTime(2026, 2, 17),
                    Language = "en",
                    PageCount = 273,
                    ThumbnailNormal =
                        "http://books.google.com/books/content?id=DE1nEQAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
                    ThumbnailSmall =
                        "http://books.google.com/books/content?id=DE1nEQAAQBAJ&printsec=frontcover&img=1&zoom=5&edge=curl&source=gbs_api",
                },
                new Title
                {
                    ISBN = "9781250359643",
                    BookName = "99 Ways to Die: And How to Avoid Them",
                    SubTitle = "And How to Avoid Them",
                    Description =
                        "An illuminating, hilarious, and practical guide to 99 of the most terrifying ways to die and how to avoid them from an emergency medicine doctor. Dr. Ashely Alker is a self-described death escapologist—or, in more familiar terms, an emergency medicine doctor. She has seen it all, from flesh-eating bacteria to the work of a serial killer to the more mundane but no less deadly, and her work outwitting the end has uniquely prepared her to write this book. Dr. Alker manages to shock readers while making them laugh, educating them on how to outsmart a wide range of deadly situations and conditions. Many of the chapters include stories from her experiences in life and medicine, at times heartwarming, others heartbreaking. Sections include explorations of sex, poison, drugs, biological warfare, disease, animals, crime, the elements, and much more. An Anthony Bourdain-style greatest hits tour of death, 99 Ways to Die is entertaining while it informs. Full of valuable advice and wild stories, this riveting read might just save your life.",
                    Publisher = "St. Martin's Press",
                    PublishedDate = new DateTime(2026, 1, 13),
                    Language = "en",
                    PageCount = 0,
                    ISBN10 = "1250359643",
                    ThumbnailNormal =
                        "http://books.google.com/books/content?id=lZ9eEQAAQBAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api",
                    ThumbnailSmall =
                        "http://books.google.com/books/content?id=lZ9eEQAAQBAJ&printsec=frontcover&img=1&zoom=5&source=gbs_api",
                }
            );

        modelBuilder
            .Entity<Author>()
            .HasData(
                new Author { AuthorID = 1, AuthorName = "Katy Brent" },
                new Author { AuthorID = 2, AuthorName = "Jenny Lawson" },
                new Author { AuthorID = 3, AuthorName = "Fergus Craig" },
                new Author { AuthorID = 4, AuthorName = "Ashely Alker" }
            );

        modelBuilder
            .Entity<BookAuthor>()
            .HasData(
                new BookAuthor { AuthorID = 1, ISBN = "9780008536695" },
                new BookAuthor { AuthorID = 2, ISBN = "9780425261019" },
                new BookAuthor { AuthorID = 3, ISBN = "9798217189052" },
                new BookAuthor { AuthorID = 4, ISBN = "9781250359643" }
            );

        modelBuilder
            .Entity<BookGenre>()
            .HasData(
                new BookGenre { Genre = "Fiction", ISBN = "9780008536695" },
                new BookGenre { Genre = "Biography & Autobiography", ISBN = "9780425261019" },
                new BookGenre { Genre = "Fiction", ISBN = "9798217189052" },
                new BookGenre { Genre = "Health & Fitness", ISBN = "9781250359643" }
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
                },
                new User
                {
                    UserID = 004,
                    Name = "Nikolaj",
                    Email = "nikolajbo@gmail.com",
                },
                new User
                {
                    UserID = 005,
                    Name = "Mustafa",
                    Email = "mustafabaker2970@gmail.com",
                },
                new User
                {
                    UserID = 006,
                    Name = "Patrick",
                    Email = "pmachalet@gmail.com",
                }
            );

        modelBuilder
            .Entity<UserBook>()
            .HasData(
                new UserBook { ISBN = "9780008536695", UserID = 001 },
                new UserBook { ISBN = "9780425261019", UserID = 001 },
                new UserBook { ISBN = "9798217189052", UserID = 003 },
                new UserBook { ISBN = "9780425261019", UserID = 003 },
                new UserBook { ISBN = "9780425261019", UserID = 002 },
                new UserBook { ISBN = "9781250359643", UserID = 002 }
            );
    }
}
