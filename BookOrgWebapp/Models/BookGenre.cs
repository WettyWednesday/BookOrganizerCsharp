using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class BookGenre
{
    public string Genre { get; set; }
    public string ISBN { get; set; }
    public Title Title { get; set; } = null!;
}
