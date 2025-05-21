using MoviesApp.Core.Interfaces;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class MovieCategoriesRepository : GenericRepository<MovieCategory> ,IMovieCategoriesRepository
    {
        public MovieCategoriesRepository(MoviesContext context) : base(context) 
        {
            
        }

        public async Task DeleteByMovieIdAsync(int movieId)
        {
            var records =  base._context.MovieCategories.Where(m => m.MovieId == movieId);
            base._context.MovieCategories.RemoveRange(records);
        }
    }
}
