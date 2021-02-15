using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineBookstore.Models
{
    //Inherit DbContext from EFCore
    public class BookstoreDbContext : DbContext
    {
        //Property for set of books
        public DbSet<Book>Books { get; set; }

        //Constructor - inherit base(options) for each instance of object built
        public BookstoreDbContext (DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }
    }
}
