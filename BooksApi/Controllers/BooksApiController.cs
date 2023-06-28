using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksApiController : ControllerBase
    {
        private readonly BooksDbContext _dbContext;

        public BooksApiController(BooksDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Books);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            if(!ModelState.IsValid || book == null)
            {
                return BadRequest();
            }

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }

        [HttpDelete]
        public IActionResult Delete(string guid)
        {
            //check for valid guid  
           
            if(string.IsNullOrEmpty(guid))
            {
                return BadRequest();
            }
            var guidCheck = System.Guid.TryParse(guid, out System.Guid guidValue);
            if (!guidCheck)
            {
                return BadRequest();
            }

            var book = _dbContext.Books.Find(guidValue);
            if(book == null)
            {
                return NotFound();
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }

        [HttpPut]
        public IActionResult Put(Book book)
        {
            if(!ModelState.IsValid || book == null)
            {
                return BadRequest();
            }

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
            return Ok(book);
        }
    }
}
