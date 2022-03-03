using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Cart
    {
        //Create a list of the BookLineItems in the cart
        public List<BookLineItem> Items { get; set; } = new List<BookLineItem>();

        //Adding books to the cart
        public virtual void AddBook(Book b, int qty)
        {
            //Compare BookIds to see if the book is already in the cart or not
            BookLineItem line = Items
                .Where(x => x.Book.BookId == b.BookId)
                .FirstOrDefault();

            //If the book is not in the cart, add it. If it is, update the quantity
            if (line == null)
            {
                Items.Add(new BookLineItem
                {
                    Book = b,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        //Method to remove a Book from the cart
        public virtual void RemoveBook(Book b)
        {
            Items.RemoveAll(x => x.Book.BookId == b.BookId);  //Find remove the Book with the matching Id
        }

        //Method to clear the cart
        public virtual void ClearCart()
        {
            Items.Clear();  //Clear all books
        }


        //Calculate total function
        public double CalcTotal()
        {
            double sum = Items.Sum(x => x.Quantity * 25);  //Each book will cost $25 for now
            return sum;
        }
    }
    
    //Adding new items to our cart
    public class BookLineItem
    {
        [Key]
        public int LineId { get; set; }  //Make this the primary key
        public Book Book { get; set; }  //Instance of the Book model
        public int Quantity { get; set; }
    }
}
