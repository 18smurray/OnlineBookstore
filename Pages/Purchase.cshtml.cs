using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookstore.Models;
using OnlineBookstore.Infrastructure;

namespace OnlineBookstore.Pages
{
    public class PurchaseModel : PageModel
    {
        private iBookstoreRepository repository;

        //Constructor
        public PurchaseModel(iBookstoreRepository repo, Cart cartservice)
        {
            repository = repo;
            Cart = cartservice;
        }

        //Information the model will keep track of
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        //Method for retrieving Cart data and getting return url
        //If nothing exists in the session cart assoc. with the key "cart", then create a new Cart
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        //Method for setting/updating Cart data
        //bookId must match what the asp-for= of the partial view is set to
        public IActionResult OnPost(long bookId, string returnUrl)
        {
            //Using bookid passed in, find the book associated with it and set in book var
            Book book = repository.Books.FirstOrDefault(b => b.BookId == bookId);
            Cart.AddItem(book, 1);
            return RedirectToPage(new { returnUrl = returnUrl });

            /*
            //Get the Cart info from the session; if no cart, create one
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            //Call the method for adding a book to the cart
            Cart.AddItem(book, 1);
            //Set the cart data in the session with "cart" as the key and Cart as the object
            HttpContext.Session.SetJson("cart", Cart);
            //Sets new returnUrl based on what parameter was passed
            return RedirectToPage(new { returnUrl = returnUrl });
            */
        }

        //Method for removing on post - handler for receiving the req and modifying the cart
        //Referenced by the asp-page-handler : asp-page-handler="Remove"
        public IActionResult OnPostRemove(long bookId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl =>
                cl.Book.BookId == bookId).Book);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

    }
}