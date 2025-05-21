using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Core.DTO
{
    public record ReviewDTO
    {
        public int Id { get; set; }
        [Range(1, 10)]
        public decimal Rating { get; set; }
        public string? Comment { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
} 