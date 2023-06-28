using BooksApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi
{
    public class BooksDbContext: DbContext
    {
        //private readonly DbContext _dbContext;

        public BooksDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        
    }
}
