using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public interface iBookstoreRepository
    {
        //Put info into a class easy to query from (similar to IEnumerable)
        IQueryable<Book>Books { get; }
    }
}
