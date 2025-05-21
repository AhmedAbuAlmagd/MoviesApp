using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.DTO
{
    public record UpdateMovieDTO  : AddMovieDTO
    {
        public int Id { get; set; } 
    }
}
