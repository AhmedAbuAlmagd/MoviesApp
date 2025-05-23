using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Data.Interfaces;
using System.Security.Claims;

namespace MoviesApp.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : BaseController
    {
        public WatchlistController(IUnitOfWork unit , IMapper mapper): base(unit, mapper) 
        {
            
        }


        [HttpGet]
        public async Task<IActionResult> GetUserWatchlist()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var watchlist = await _unit.WatchListRepository.GetUserWatchlistAsync(userId);
            return Ok(watchlist);
        }

        [HttpPost("{movieId}")]
        public async Task<IActionResult> AddToWatchlist(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _unit.WatchListRepository.AddToWatchlistAsync(userId, movieId);
            if (!result)
                return BadRequest("Movie is already in watchlist");

            return Ok();
        }

        [HttpDelete("{movieId}")]
        public async Task<IActionResult> RemoveFromWatchlist(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _unit.WatchListRepository.RemoveFromWatchlistAsync(userId, movieId);
            if (!result)
                return NotFound("Movie is not in watchlist");

            return Ok();
        }

        [HttpGet("Check/{movieId}")]
        public async Task<IActionResult> CheckWatchlistStatus(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var exists = await _unit.WatchListRepository.IsInWatchlistAsync(userId, movieId);
            return Ok(exists);
        }

    }
}
