using Microsoft.AspNetCore.Http;
using MoviesApp.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.DTO
{
    public record AddMovieDTO
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? Poster { get; set; }
        public IFormFile? Trailer { get; set; }
        [Range(1,10)]
        public decimal? Rating { get; set; }
        public List<int>? CategoryIds{ get; set; } 
    }
}
