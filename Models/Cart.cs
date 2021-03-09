using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore.Models
{
    public class Cart
    {
        //Create list called Lines to hold CartLines - each CartLine represents a line in the cart display
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        //Method for adding new books to the cart
        public virtual void AddItem (Book bk, int qty)
        {
            //Var line is set to results of query for CartLines that have the same bookid as the book it's attempting
            //to add (Ensure the same book isn't added on two separate lines)
            //Grab the first (and only) instance of the returned results
            CartLine line = Lines.Where(b => b.Book.BookId == bk.BookId).FirstOrDefault();

            //If the book isn't already in the line, add a new CartLine for it
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = bk,
                    Quantity = qty
                });
            }
            //If book already exists in the cart, update the quantity without creating a new CartLine
            //Rember line refers to an existing CartLine with the same bookid as the book we want to add
            else
            {
                line.Quantity += qty;
            }
        }

        //Method for removing books from the cart
        //Removes the CartLine in Lines where the bookid matches the book we're removing
        public virtual void RemoveLine(Book bk) =>
            Lines.RemoveAll(b => b.Book.BookId == bk.BookId);

        //Method for removing everything from the cart
        //Clear all the CartLines out of the Lines list
        public virtual void Clear() => Lines.Clear();

        //Get total cost of cart
        public double ComputeTotalSum() =>
            //For each Cartline in Lines, take the sum of the book price multiplied by the quantity
            Lines.Sum(cl => cl.Book.Price * cl.Quantity);

        //Object for holding three pieces of data - bundled together (one line in the cart)
        public class CartLine
        {
            public int CartLineId { get; set; }
            public Book Book { get; set; }
            public int Quantity { get; set; }
        }
    }
}
