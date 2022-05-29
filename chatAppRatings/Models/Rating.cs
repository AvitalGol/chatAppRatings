using System.ComponentModel.DataAnnotations;

namespace chatAppRatings.Models
{
    public class Rating
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 5)]
        public int NumericRating { get; set; }

        [Required]
        public string FeedBack { get; set; }

        public string Time { get; set; }
    }
}
