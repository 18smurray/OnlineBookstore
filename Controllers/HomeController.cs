using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineBookstore.Models;
using OnlineBookstore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private iBookstoreRepository _repository;

        //New var for keeping track of how many books to display per page
        public int PageSize = 5;

        public HomeController(ILogger<HomeController> logger, iBookstoreRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //Expects page number as a parameter; default is page 1
        //Add another parameter for keeping track of category filter (passed in url request)
        public IActionResult Index(string category, int pageNum = 1) 
        {
            return View(
                // Has Books and Paging Info
                new BookListViewModel
                {
                    Books = _repository.Books
                    //Filtering by cateogry; if no category passed, return all books, 
                    //otherwise return books where the category matches the category filter passed (can't be both)
                    .Where(b => category == null || b.Category == category)
                    .OrderBy(b => b.BookId) //Orders books by their ids
                    .Skip((pageNum - 1) * PageSize) //Skip items depending on the current page
                    .Take(PageSize) //Return the correct number of items

                    //Two specific pieces of information - Books and PagingInfo
                    ,

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = pageNum,
                        ItemsPerPage = PageSize,
                        //TotalNumItems = _repository.Books.Count()
                        //Have to set number of pages based on number of books returned (may be filtered) not always total count
                        TotalNumItems = 
                            category == null ? _repository.Books.Count() //If category is null, use total count of books
                            :
                            _repository.Books.Where(x => x.Category == category).Count()
                            //If category is passed, take count of books with that category to determine number of pages
                    },

                    //Track category for filtering
                    CurrentCategory = category
                });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
