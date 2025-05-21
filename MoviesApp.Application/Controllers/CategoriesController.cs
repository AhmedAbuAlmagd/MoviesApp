using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Core.DTO;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;

namespace MoviesApp.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
 
        public CategoriesController(IUnitOfWork unitOfWork , IMapper mapper) : base(unitOfWork , mapper)
        {
        
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categotries = await _unit.CategoryRepository.GetAllAsync();
                if (categotries == null)
                    return NotFound();
                return Ok(categotries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category =await _unit.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize("Admin")]
        public async Task<IActionResult> Add(CategoryDTO category)
        {
            try
            {
                 await _unit.CategoryRepository.AddAsync(_mapper.Map<Category>(category));
                 await _unit.CategoryRepository.SaveAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Edit")]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit( int id,UpdateCategoryDTO category) 
        {
           try
            {
                if(id != category.id)
                    return BadRequest("Ids are not equals");
                await _unit.CategoryRepository.UpdateAsync(_mapper.Map<Category>(category));
                await _unit.CategoryRepository.SaveAsync();
                return Ok(category);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (_unit.CategoryRepository.GetByIdAsync(id) == null)
                    return NotFound();
                await _unit.CategoryRepository.DeleteAsync(id);
                await _unit.CategoryRepository.SaveAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
