using MaxiShop.Web.Data;
using MaxiShop.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public ActionResult Create([FromBody]Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        [Route("Details")]
        public ActionResult ReadAll()
        {
            var categories = _dbContext.Categories.ToList();
            
            return Ok(categories);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult Read(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id==id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut]
        public ActionResult Update([FromBody] Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var result = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(result);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
