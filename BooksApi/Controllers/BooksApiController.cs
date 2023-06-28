using BooksApi.Models;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Delete(Book book)
        {
            if(!ModelState.IsValid || book == null)
            {
                return BadRequest();
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
