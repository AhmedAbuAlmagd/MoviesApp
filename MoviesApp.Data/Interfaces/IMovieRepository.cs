using MoviesApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Data.Interfaces
{
    public interface IMovieRepository :IRepository<Movie>
    {
        Task<List<Movie>> GetAllAsync(string sort,int? catId, string searchWord, int pageSize , int pageNumber);
        Task<int> GetCountAsync();
    }
}
