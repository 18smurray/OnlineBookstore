using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Class for keeping track of paging information

namespace OnlineBookstore.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalNumItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        //Calculate total pages by dividing total number of books by items per page; round up 
        public int TotalPages => (int)(Math.Ceiling((decimal)TotalNumItems / ItemsPerPage));
    }
}
