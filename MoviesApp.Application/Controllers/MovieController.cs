using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Application.Helper;
using MoviesApp.Core.DTO;
using MoviesApp.Core.Services;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;

namespace MoviesApp.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        public MovieController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {

        }

        [HttpGet("getall")]
        public async Task<IActionResult>GetAll(string? sort ,int? catId, string? searchWord, int pageSize =6 , int pageNumber =1)
        {
            try
            {
                Console.WriteLine($"Received request - Page: {pageNumber}, Size: {pageSize}, Category: {catId}, Search: {searchWord}, Sort: {sort}");
                
                List<Movie> movies = await _unit.MovieRepository.GetAllAsync(sort,catId,searchWord, pageSize , pageNumber);
                var MoviesDto = _mapper.Map<List<MovieDTO>>(movies);
                int totalCount = await _unit.MovieRepository.GetFilteredCountAsync(catId, searchWord);
                
                Console.WriteLine($"Returning - Total Count: {totalCount}, Movies Count: {movies.Count}");
                
                return Ok(new Pagination<MovieDTO>(pageNumber,pageSize,totalCount,MoviesDto));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getById/{id:int}")]
        [RequestSizeLimit(200_000_000)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var movie = await _unit.MovieRepository.GetByIdAsync(id);
                if (movie == null)
                    return NotFound();

                return Ok(_mapper.Map<MovieDTO>(movie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [RequestSizeLimit(200_000_000)]
        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromForm] AddMovieDTO movieDto)
        {
            try
            {
                Movie movie = _mapper.Map<Movie>(movieDto);

                if (movieDto.Poster != null)
                    movie.Poster = await _unit.ImageService.AddMedia(movieDto.Poster, "image");

                if (movieDto.Trailer != null)
                    movie.Trailer = await _unit.ImageService.AddMedia(movieDto.Trailer, "video");

                

                if (movieDto.CategoryIds != null && movieDto.CategoryIds.Any())
                {
                    movie.MovieCategories = movieDto.CategoryIds
                        .Select(id => new MovieCategory { CategoryId = id, MovieId = movie.Id })
                        .ToList();
                }

                await _unit.MovieRepository.UpdateAsync(movie);
                await _unit.MovieRepository.SaveAsync();
                return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [RequestSizeLimit(200_000_000)]
        [HttpPut("Edit")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Edit([FromForm]UpdateMovieDTO movieDto)
        {

            try
            {
                Movie oldMovie = await _unit.MovieRepository.GetByIdAsync(movieDto.Id);
                if (oldMovie == null)
                    return NotFound("Invalid Id");
                if (movieDto.Poster != null)
                    oldMovie.Poster = await _unit.ImageService.AddMedia(movieDto.Poster, "image");

                if (movieDto.Trailer != null)
                    oldMovie.Trailer = await _unit.ImageService.AddMedia(movieDto.Trailer, "video");

             
                
                if (movieDto.CategoryIds != null && movieDto.CategoryIds.Any())
                {
                    await _unit.MovieCategoriesRepository.DeleteByMovieIdAsync(movieDto.Id);
                    oldMovie.MovieCategories = movieDto.CategoryIds
                        .Select(id => new MovieCategory { CategoryId = id, MovieId = oldMovie.Id })
                        .ToList();
                }

                Movie movie = _mapper.Map(movieDto, oldMovie);

                await _unit.MovieRepository.SaveAsync();

                return Ok(_mapper.Map<MovieDTO>(oldMovie));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [RequestSizeLimit(200_000_000)]
        [HttpDelete("Delete/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unit.MovieRepository.DeleteAsync(id);
                await _unit.MovieRepository.SaveAsync();
                return Ok("Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

    }
}
