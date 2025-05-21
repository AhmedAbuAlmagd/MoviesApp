using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Application.Helper;
using MoviesApp.Core.DTO;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;

namespace MoviesApp.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        public ReviewController(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(int movieId)
        {
            try
            {
                var reviews = await _unit.ReviewRepository.GetAllAsync();
                var movieReviews = reviews.Where(r => r.MovieId == movieId).ToList();
                var reviewsDto = _mapper.Map<List<ReviewDTO>>(movieReviews);
                return Ok(reviewsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var review = await _unit.ReviewRepository.GetByIdAsync(id);
                if (review == null)
                    return NotFound();

                return Ok(_mapper.Map<ReviewDTO>(review));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize("Admin")]
        public async Task<IActionResult> Add(AddReviewDTO reviewDto)
        {
            try
            {
                var review = _mapper.Map<Review>(reviewDto);
                review.UserId = User.FindFirst("uid")?.Value;
                
                await _unit.ReviewRepository.AddAsync(review);
                await _unit.ReviewRepository.SaveAsync();

                // Update movie rating
                var movie = await _unit.MovieRepository.GetByIdAsync(review.MovieId);
                if (movie != null)
                {
                    var movieReviews = await _unit.ReviewRepository.GetAllAsync();
                    var movieReviewsList = movieReviews.Where(r => r.MovieId == movie.Id).ToList();
                    movie.Rating = movieReviewsList.Average(r => r.Rating);
                    await _unit.MovieRepository.UpdateAsync(movie);
                    await _unit.MovieRepository.SaveAsync();
                }

                return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [Authorize]
        [HttpPut("Edit")]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(UpdateReviewDTO reviewDto)
        {
            try
            {
                var oldReview = await _unit.ReviewRepository.GetByIdAsync(reviewDto.Id);
                if (oldReview == null)
                    return NotFound("Invalid Id");

                if (oldReview.UserId != User.FindFirst("uid")?.Value)
                    return Unauthorized("You can only edit your own reviews");

                var review = _mapper.Map(reviewDto, oldReview);
                await _unit.ReviewRepository.UpdateAsync(review);
                await _unit.ReviewRepository.SaveAsync();

                // Update movie rating
                var movie = await _unit.MovieRepository.GetByIdAsync(review.MovieId);
                if (movie != null)
                {
                    var movieReviews = await _unit.ReviewRepository.GetAllAsync();
                    var movieReviewsList = movieReviews.Where(r => r.MovieId == movie.Id).ToList();
                    movie.Rating = movieReviewsList.Average(r => r.Rating);
                    await _unit.MovieRepository.UpdateAsync(movie);
                    await _unit.MovieRepository.SaveAsync();
                }

                return Ok(_mapper.Map<ReviewDTO>(review));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("Delete/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var review = await _unit.ReviewRepository.GetByIdAsync(id);
                if (review == null)
                    return NotFound("Invalid Id");

                if (review.UserId != User.FindFirst("uid")?.Value)
                    return Unauthorized("You can only delete your own reviews");

                await _unit.ReviewRepository.DeleteAsync(id);
                await _unit.ReviewRepository.SaveAsync();

                // Update movie rating
                var movie = await _unit.MovieRepository.GetByIdAsync(review.MovieId);
                if (movie != null)
                {
                    var movieReviews = await _unit.ReviewRepository.GetAllAsync();
                    var movieReviewsList = movieReviews.Where(r => r.MovieId == movie.Id).ToList();
                    movie.Rating = movieReviewsList.Any() ? movieReviewsList.Average(r => r.Rating) : 0;
                    await _unit.MovieRepository.UpdateAsync(movie);
                    await _unit.MovieRepository.SaveAsync();
                }

                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
