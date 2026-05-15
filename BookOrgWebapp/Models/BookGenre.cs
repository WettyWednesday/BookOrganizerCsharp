using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class BookGenre
{
    public required string Genre { get; set; }
    public required string ISBN { get; set; }
    public Title Title { get; set; } = null!;
}
