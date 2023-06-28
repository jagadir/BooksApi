using BooksApi;
using BooksApi.Controllers;
using BooksApi.Models;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksApiTests
{
    public class BooksApiTests
    {
        //reference
        //https://github.com/rgvlee/EntityFrameworkCore.Testing

        [Fact]
        public void GetAllTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BooksDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mockedDbContext = Create.MockedDbContextFor<BooksDbContext>(dbContextOptions);

            //Arrange
            var controller = new BooksApiController(mockedDbContext);
            //Act
            var result = controller.GetAll();
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var book = new Book { Author = "Test Author", Title = "Test Title", Id=global::System.Guid.NewGuid() };

            var dbContextOptions = new DbContextOptionsBuilder<BooksDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mockedDbContext = Create.MockedDbContextFor<BooksDbContext>(dbContextOptions);
            mockedDbContext.Books.Add(book);
            var controller = new BooksApiController(mockedDbContext);
            //Act
            var result = controller.GetById(book.Id.ToString());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult result1 = ((OkObjectResult)result);
            Book? bookResult = result1.Value as Book;
            Assert.Equal(book?.Id, bookResult?.Id);
        }

        [Fact]
        public void CreateBookTest()
        {
            //Arrange
            var book = new Book { Author = "Test Author", Title = "Test Title", Id = global::System.Guid.NewGuid() };

            var dbContextOptions = new DbContextOptionsBuilder<BooksDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mockedDbContext = Create.MockedDbContextFor<BooksDbContext>(dbContextOptions);
            var controller = new BooksApiController(mockedDbContext);
            //Act
            var result = controller.Create(book);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult result1 = ((OkObjectResult)result);
            Book? bookResult = result1.Value as Book;
            Assert.Equal(book?.Id, bookResult?.Id);
        }

        [Fact]
        public void UpdateBookTest()
        {
            //Arrange
            var book = new Book { Author = "Test Author", Title = "Test Title", Id = Guid.NewGuid() };

            var dbContextOptions = new DbContextOptionsBuilder<BooksDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mockedDbContext = Create.MockedDbContextFor<BooksDbContext>(dbContextOptions);
           
           
            var controller = new BooksApiController(mockedDbContext);
            controller.Create(book);

            book.Author = "Test Author Updated";
            book.Title = "Test Title Updated";
            
            //Act
            var result = controller.Update(book);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult result1 = ((OkObjectResult)result);
            Book? bookResult = result1.Value as Book;
            Assert.Equal(book?.Id, bookResult?.Id);
            Assert.NotEqual("Test Author", bookResult?.Author);
            Assert.NotEqual("Test Title", bookResult?.Title);
            
        }

        [Fact]
        public void DeleteBookTest()
        {
            //Arrange
            var book = new Book { Author = "Test Author", Title = "Test Title", Id = global::System.Guid.NewGuid() };

            var dbContextOptions = new DbContextOptionsBuilder<BooksDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var mockedDbContext = Create.MockedDbContextFor<BooksDbContext>(dbContextOptions);
            mockedDbContext.Books.Add(book);
            var controller = new BooksApiController(mockedDbContext);
            //Act
            var result = controller.Delete(book.Id.ToString());
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            OkObjectResult result1 = ((OkObjectResult)result);
            Book? bookResult = result1.Value as Book;
            Assert.Equal(book?.Id, bookResult?.Id);
        }

    }
}