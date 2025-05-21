using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.Interfaces
{
    public interface IMovieCategoriesRepository : IRepository<MovieCategory>
    {
        Task DeleteByMovieIdAsync(int movieId);
    }
}
