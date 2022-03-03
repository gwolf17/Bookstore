using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Infrastructure;
using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookstore.Pages
{
    public class BuyModel : PageModel
    {
        //Instance of IBookstoreRepository
        private IBookstoreRepository repo { get; set; }

        //Create cart and Url variables
        public Cart Cart { get; set; }  //Instance of the Cart model
        public string ReturnUrl { get; set; }

        //Constructor
        public BuyModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            Cart = c;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";  //If not null, set ReturnUrl to the url passed in. Otherwise, use the home route (/)
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);  //Find the corresponding book based on BookId

            Cart.AddBook(b, 1); //Add an item and send in the Book and quantity

            return RedirectToPage(new { ReturnUrl = returnUrl });  //Redirect to the page the user was on before
        }

        public IActionResult OnPostDelete (int bookId, string returnUrl)
        {
            //Remove the Book object that has the same Id as the bookId being passed in
            Cart.RemoveBook(Cart.Items.First(x => x.Book.BookId == bookId).Book);

            //Redirect the user to the returnUrl page
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
