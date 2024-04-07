using MaxiShop.Domine.Contracts;
using MaxiShop.Domine.Models;
using MaxiShop.Infrastructure.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Category category)
        {
            await _categoryRepository.CreateAsync(category);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public async Task<ActionResult> ReadAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            
            return Ok(categories);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult> Read(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] Category category)
        {
            await _categoryRepository.UpdateAsync(category);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var result = await _categoryRepository.GetByIdAsync(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteAsync(result);
            return NoContent();
        }
    }
}  
