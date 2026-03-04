using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class Title
{
    [Key]
    [RegularExpression(@"^\d{13}$")]
    [Column(TypeName = "nvarchar(13)")]
    public string ISBN { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(60)")]
    public string BookName { get; set; }

    public ICollection<Book> Books { get; set; }
}