using Microsoft.EntityFrameworkCore;
using MoviesApp.Core.DTO;
using MoviesApp.Core.Interfaces;
using MoviesApp.Core.Models;
using MoviesApp.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class WatchlistRepository : IWatchListRepository
    {
        private readonly MoviesContext _context;

        public WatchlistRepository(MoviesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieDTO>> GetUserWatchlistAsync(string userId)
        {
            var watchlists = await _context.watchlists
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.AddedDate)
                .ToListAsync();

            return watchlists.Select(w => new WatchListMovieDTO
            {
                Id = w.Movie.Id,
                Title = w.Movie.Title,
                Description = w.Movie.Description,
                ReleaseDate = w.Movie.ReleaseDate,
                Rating = w.Movie.Rating,
                Poster = w.Movie.Poster,
                Trailer = w.Movie.Trailer,
                Categories = w.Movie.MovieCategories
                 .Select(mc => mc.Category)
                 .Select(c => new UpdateCategoryDTO(c.Id, c.Name))
                 .ToList()
            });
        }

        public async Task<bool> AddToWatchlistAsync(string userId, int movieId)
        {
            var exists = await _context.watchlists
                .AnyAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (exists)
                return false;

            var watchlist = new Watchlist
            {
                UserId = userId,
                MovieId = movieId,
                AddedDate = System.DateTime.UtcNow
            };

            _context.watchlists.Add(watchlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWatchlistAsync(string userId, int movieId)
        {
            var watchlist = await _context.watchlists
                .FirstOrDefaultAsync(w => w.UserId == userId && w.MovieId == movieId);

            if (watchlist == null)
                return false;

            _context.watchlists.Remove(watchlist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsInWatchlistAsync(string userId, int movieId)
        {
            return await _context.watchlists
                .AnyAsync(w => w.UserId == userId && w.MovieId == movieId);
        }

    }
}
