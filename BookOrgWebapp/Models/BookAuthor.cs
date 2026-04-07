using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class BookAuthor
{
    public int AuthorID { get; set; }
    public string ISBN { get; set; }
    public Title Title { get; set; } = null!;
    public Author Author { get; set; } = null!;
}
