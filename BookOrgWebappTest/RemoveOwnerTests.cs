using System;
using System.Linq;
using BookOrgWebapp.Components.Pages;
using BookOrgWebapp.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace BookOrgWebAppTest;

public class RemoveOwnerTests : BunitContext
{
    private readonly AppDbContext _db;

    public RemoveOwnerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _db = new AppDbContext(options);
        _db.Database.EnsureCreated();

        Services.AddSingleton(_db);
        AddAuthorization().SetAuthorized("TestUser");
    }

    [Fact]
    public void ClickingRemoveOnOwner_RemovesUserBookFromDatabase()
    {
        var seededIsbn = "9780008536695";
        var seededUserId = 1;

        _db.UserBooks.Should()
            .Contain(ub => ub.UserID == seededUserId && ub.ISBN == seededIsbn);

        var nav = Services.GetRequiredService<NavigationManager>();
        nav.NavigateTo("/search?searchTerm=How%20to%20Kill%20Men");

        var cut = Render<Search>();

        cut.WaitForState(() => cut.Markup.Contains("How to Kill Men"));
        cut.Find(".book-card").Click();

        cut.WaitForState(() => cut.Markup.Contains("Owners"));
        cut.Find(".owner-list .remove-btn").Click();

        cut.WaitForAssertion(() =>
            _db.UserBooks.Any(ub => ub.UserID == seededUserId && ub.ISBN == seededIsbn)
                .Should().BeFalse());
    }

    [Fact]
    public void RemovingLastOwner_AlsoRemovesTheTitle()
    {
        var isbn = "9781250359643";

        var nav = Services.GetRequiredService<NavigationManager>();
        nav.NavigateTo("/search?searchTerm=99%20Ways%20to%20Die");

        var cut = Render<Search>();

        cut.WaitForState(() => cut.Markup.Contains("99 Ways to Die"));
        cut.Find(".book-card").Click();
        cut.WaitForState(() => cut.Markup.Contains("Owners"));
        cut.Find(".owner-list .remove-btn").Click();

        cut.WaitForAssertion(() =>
        {
            _db.UserBooks.Any(ub => ub.ISBN == isbn).Should().BeFalse();
            _db.Titles.Any(t => t.ISBN == isbn).Should().BeFalse();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _db.Dispose();
        base.Dispose(disposing);
    }
}