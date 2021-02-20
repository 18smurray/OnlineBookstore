using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class Book
    {
        //Primary Key property; will autogenerate with creation of new Book instances
        [Key]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string AuthorFirstName { get; set; }
   
        public string? AuthorMiddleName { get; set; }
        [Required]
        public string AuthorLastName { get; set; }
        [Required]
        public string Publisher { get; set; }

        //Ensure valid ISBN format using regular expression
        [Required]
        [RegularExpression(@"^\d{3}-\d{10}$", ErrorMessage = "Must have valid ISBN format")]
        public string ISBN { get; set; }

        [Required]
        public string Classification { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }

        //New Database field added - number of pages in the book
        [Required]
        public int PageCount { get; set; }
    }
}
