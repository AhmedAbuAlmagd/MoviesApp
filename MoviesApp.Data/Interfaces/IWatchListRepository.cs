using MoviesApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Core.Interfaces
{
    public interface IWatchListRepository 
    {
        Task<IEnumerable<MovieDTO>> GetUserWatchlistAsync(string userId);
        Task<bool> AddToWatchlistAsync(string userId, int movieId);
        Task<bool> RemoveFromWatchlistAsync(string userId, int movieId);
        Task<bool> IsInWatchlistAsync(string userId, int movieId);
    }
}
