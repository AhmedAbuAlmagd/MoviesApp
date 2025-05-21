using Microsoft.EntityFrameworkCore;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class MovieRepository : GenericRepository<Movie> , IMovieRepository
    {
        public MovieRepository(MoviesContext context) : base(context) 
        {
            
        }

        public async Task<List<Movie>> GetAllAsync(string? sort ,int? catId ,string searchWord, int pageSize, int pageNumber)
        {
            var query = _context.Movies.AsNoTracking().AsQueryable();


            //Filtering with Word 
            if(!String.IsNullOrEmpty(searchWord))
            {
                string[] searchWords = searchWord.Split(' ');
                query = query.Where(m => searchWords.All(word =>
                    m.Title.ToLower().Contains(word.ToLower()) || m.Description.ToLower().Contains(word.ToLower())
                ));
            }
            
            //Filtering with Category 
            if(catId.HasValue)
                query = query.Where(m => m.MovieCategories.Any(c => c.CategoryId == catId));


            //Filtering With Rating 
           if(!String.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "RatingAsc":
                        query = query.OrderBy(q => q.Rating);
                        break;
                    case "RatingDesc":
                        query = query.OrderByDescending(q => q.Rating);
                        break ;
                    default :
                        query = query.OrderBy(q => q.Title);
                        break;
                }
            }

            return await query.Skip(pageSize*(pageNumber-1)).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Movies.AsNoTracking().CountAsync();
        }
    }
}
