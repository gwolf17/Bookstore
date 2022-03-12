using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Controller specifically for the Checkout process
namespace Bookstore.Controllers
{
    public class CheckoutController : Controller
    {
        private IShippingRepository repo { get; set; }  //Instance of our repository
        private Cart cart { get; set; }  //Instance of our Cart

        //Constructor (set up repositories and our cart)
        public CheckoutController (IShippingRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            //Create and send a new Purchase object
            return View(new Purchase());
        }

        [HttpPost]
        public IActionResult Checkout(Purchase purchase)  //We receive a new Purchase object
        {
            //If the cart is empty, return an error message
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            //Check to see if model is valid
            if (ModelState.IsValid)
            {
                purchase.Lines = cart.Items.ToArray();
                repo.SavePurchase(purchase);  //Save the purchase
                cart.ClearCart();  //Clear the cart

                return RedirectToPage("/PurchaseConfirmation");
            }

            return View();
        }
    }
}
