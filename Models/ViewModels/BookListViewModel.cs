using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models.ViewModels
{
    public class BookListViewModel
    {
        //Attributes to hold enumberable list of books and paging info
        //Contains two sets of information
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }

        //Attribute for tracking category filter; have to set in Controller
        public string CurrentCategory { get; set; }
    }
}
