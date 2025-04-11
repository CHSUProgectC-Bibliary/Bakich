using System.ComponentModel.DataAnnotations;

namespace BookReviewAPI.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
