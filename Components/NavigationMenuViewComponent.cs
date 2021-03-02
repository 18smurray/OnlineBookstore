using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBookstore.Models;

namespace OnlineBookstore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        //Have to bring in book repository information; variable for storing instance
        private iBookstoreRepository repository;

        //Constructor - pass repository of books as a parameter
        public NavigationMenuViewComponent(iBookstoreRepository r)
        {
            repository = r;
        }

        //View Component method called when component is added
        //Can now use Linq SQL to return book categories
        //Going to look for view on how to format the returned data - NavigationMenu Default view
        public IViewComponentResult Invoke()
        {
            //Look at category in url and set to viewbag, "?" means it could be null
            //Use Viewbag to pass info between controller/view
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(repository.Books
                .Select(x => x.Category)    //Select category from each book in repository
                .Distinct()                 //Returns only distinct categories
                .OrderBy(x => x));          //Order by natural order of data...
        }
    }
}
