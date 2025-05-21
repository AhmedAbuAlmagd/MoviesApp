using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Poster { get; set; }
        public string? Trailer { get; set; }
        public decimal? Rating { get; set; }
        public virtual List<MovieCategory>? MovieCategories { get; set; } = new List<MovieCategory>();
    }
}
