using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.DTO
{
    public record MovieDTO
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string? Trailer { get; set; }
        [Range(1, 10)]
        public decimal? Rating { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }   
    
}
