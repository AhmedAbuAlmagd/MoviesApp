using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.DTO
{
    public record WatchListMovieDTO :MovieDTO
    {
        public List<UpdateCategoryDTO> Categories { get; set; }

    }
}
