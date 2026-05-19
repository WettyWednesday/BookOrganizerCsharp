using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class UserBook
{
    public required string ISBN { get; set; }
    public int UserID { get; set; }

    public Title Title { get; set; } = null!;
    public User User { get; set; } = null!;
}
