using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class Title
{
    [Key]
    [RegularExpression(@"^\d{13}$")]
    [Column(TypeName = "nvarchar(13)")]
    public required string ISBN { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(120)")]
    public string BookName { get; set; } = null!;

    public string? SubTitle { get; set; }

    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public DateTime? PublishedDate { get; set; }
    public string? Language { get; set; }
    public int? PageCount { get; set; }

    [RegularExpression(@"^\d{10}$")]
    [Column(TypeName = "nvarchar(10)")]
    public string? ISBN10 { get; set; }
    public string? ThumbnailSmall { get; set; }
    public string? ThumbnailNormal { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookGenre> BookGenres { get; set; } = new List<BookGenre>();

    public ICollection<UserBook> UserBooks { get; set; } = new List<UserBook>();
}
