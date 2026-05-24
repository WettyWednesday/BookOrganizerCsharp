using System;
using System.Collections.Generic;
using BookOrgWebapp.Components.Pages;
using BookOrgWebapp.Data;
using BookOrgWebapp.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;
using System.Net.Http;

namespace BookOrgWebAppTest;

public class AddBookTests : BunitContext
{
    private readonly AppDbContext _db;
    private readonly Mock<GoogleBooksService> _booksService;

    private const string ValidIsbn = "9780747532699";
    private const string BookTitle = "Harry Potter and the Philosopher's Stone";

    public AddBookTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _db = new AppDbContext(options);
        _db.Database.EnsureCreated();

        var config = new ConfigurationBuilder().Build();
        _booksService = new Mock<GoogleBooksService>(
            MockBehavior.Strict, new HttpClient(), config);

        Services.AddSingleton(_db);
        Services.AddSingleton(_booksService.Object);

        AddAuthorization().SetAuthorized("TestUser");
    }

    private IRenderedComponent<Home> RenderHomepage()
        => Render<Home>();

    [Fact]
    public void InvalidIsbn_ShowsValidationMessage()
    {
        var cut = RenderHomepage();

        cut.Find("input.search-input").Change("12345");
        cut.Find("button.search-button").Click();

        cut.Markup.Should().Contain("Please enter a valid ISBN-10 or ISBN-13.");
    }

    [Fact]
    public void UnknownIsbn_ShowsBookNotFoundMessage()
    {
        _booksService
            .Setup(s => s.GetBookByISBN(It.IsAny<string>()))
            .ReturnsAsync((new GoogleBooksResponse { Items = new() }, HttpStatusCode.OK));

        var cut = RenderHomepage();

        cut.Find("input.search-input").Change(ValidIsbn);
        cut.Find("button.search-button").Click();

        cut.WaitForState(() => cut.Markup.Contains("Book not found."));
    }

    [Fact]
    public void ValidIsbn_ShowsBookModal()
    {
        _booksService
            .Setup(s => s.GetBookByISBN(It.IsAny<string>()))
            .ReturnsAsync((FakeResponse(BookTitle, ValidIsbn), HttpStatusCode.OK));

        var cut = RenderHomepage();

        cut.Find("input.search-input").Change(ValidIsbn);
        cut.Find("button.search-button").Click();

        cut.WaitForAssertion(() =>
        {
            cut.Markup.Should().Contain(BookTitle);
            cut.FindAll(".book-modal").Should().NotBeEmpty();
        });
    }

    [Fact]
    public void SelectingUser_AndClickingAddBook_ShowsSuccessMessage()
    {
        _booksService
            .Setup(s => s.GetBookByISBN(It.IsAny<string>()))
            .ReturnsAsync((FakeResponse(BookTitle, ValidIsbn), HttpStatusCode.OK));

        var cut = RenderHomepage();

        cut.Find("input.search-input").Change(ValidIsbn);
        cut.Find("button.search-button").Click();
        cut.WaitForState(() => cut.Markup.Contains(BookTitle));

        cut.Find("select").Change("1");
        cut.Find("button.add-button").Click();

        cut.WaitForAssertion(() =>
        {
            cut.Markup.Should().Contain("Book added successfully!");
            _db.UserBooks.Should().Contain(ub =>
                ub.UserID == 1 && ub.ISBN == ValidIsbn);
        });
    }

    [Fact]
    public void AddingBookUserAlreadyOwns_ShowsAlreadyOwnsMessage()
    {
        _db.Titles.Add(new Title { ISBN = ValidIsbn, BookName = BookTitle });
        _db.UserBooks.Add(new UserBook { UserID = 1, ISBN = ValidIsbn });
        _db.SaveChanges();

        _booksService
            .Setup(s => s.GetBookByISBN(It.IsAny<string>()))
            .ReturnsAsync((FakeResponse(BookTitle, ValidIsbn), HttpStatusCode.OK));

        var cut = RenderHomepage();

        cut.Find("input.search-input").Change(ValidIsbn);
        cut.Find("button.search-button").Click();
        cut.WaitForState(() => cut.Markup.Contains(BookTitle));

        cut.Find("select").Change("1");
        cut.Find("button.add-button").Click();

        cut.WaitForState(() => cut.Markup.Contains("This user already owns that book."));
    }

    private static GoogleBooksResponse FakeResponse(string title, string isbn13) => new()
    {
        Items = new List<Item>
        {
            new()
            {
                VolumeInfo = new VolumeInfo
                {
                    Title = title,
                    Authors = new List<string> { "J. K. Rowling" },
                    IndustryIdentifiers = new List<IndustryIdentifier>
                    {
                        new() { Type = "ISBN_13", Identifier = isbn13 }
                    }
                }
            }
        }
    };

    protected override void Dispose(bool disposing)
    {
        if (disposing) _db.Dispose();
        base.Dispose(disposing);
    }
}