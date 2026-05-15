using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class Author
{
    [Key]
    public int AuthorID { get; set; }

    [Required]
    [StringLength(120)]
    public required string AuthorName { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
