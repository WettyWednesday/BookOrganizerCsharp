using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookOrgWebapp.Models;

public class Book
{
    public string ISBN { get; set; }
    public int UserID { get; set; }

    public Title Title { get; set; }
    public User User { get; set; }
}
