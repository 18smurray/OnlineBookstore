using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class EFBookstoreRepository : iBookstoreRepository
    {
        private BookstoreDbContext _context;

        public EFBookstoreRepository (BookstoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> Books => _context.Books;
    }
}
