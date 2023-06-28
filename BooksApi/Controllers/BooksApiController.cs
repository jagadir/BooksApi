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
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Books);
        }

        [HttpGet("{guid}")]
        public IActionResult GetById(string guid)
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
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
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
        public IActionResult Update(Book book)
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
