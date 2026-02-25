using System.ComponentModel.DataAnnotations;

namespace BookOrgWebapp.Models
{
    public class Book
    {
        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string ISBN { get; set; } = string.Empty;

        [Range(0, 999)]
        public int UserID { get; set; }

        [StringLength(60)]
        public string BookName { get; set; } = string.Empty;

        public int YearOfPublication { get; set; }

        public List<string> Authors { get; set; } = new();
    }
}