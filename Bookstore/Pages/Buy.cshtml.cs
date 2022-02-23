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

        //Constructor
        public BuyModel (IBookstoreRepository temp)
        {
            repo = temp;
        }

        //Create cart and Url variables
        public Cart Cart { get; set; }  //Instance of the Cart model
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";  //If not null, set ReturnUrl to the url passed in. Otherwise, use the home route (/)
            Cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();  //Use the previously created cart, or create a new one if it is null
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);  //Find the corresponding book based on BookId

            Cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();  //Use the current cart, or create a new one if null
            Cart.AddItem(b, 1); //Add an item and send in the Book and quantity

            HttpContext.Session.SetJson("Cart", Cart);  //Set Json Cart object equal to whatever the Cart object is right now

            return RedirectToPage(new { ReturnUrl = returnUrl });  //Redirect to the page the user was on before
        }
    }
}
