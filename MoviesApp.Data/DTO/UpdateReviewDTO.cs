using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Core.DTO
{
    public record UpdateReviewDTO
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 10)]
        public decimal Rating { get; set; }
        public string? Comment { get; set; }
        [Required]
        public int MovieId { get; set; }
    }
} 