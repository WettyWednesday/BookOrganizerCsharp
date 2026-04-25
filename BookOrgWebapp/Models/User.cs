using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class User
{
    [Key]
    [Range(0, 999)]
    public int UserID { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(60)]
    [Column(TypeName = "nvarchar(60)")]
    public string Email { get; set; }
    
    public string? GoogleId { get; set; }
    public string? AvatarUrl { get; set; }

    public ICollection<UserBook> UserBooks { get; set; }
}
