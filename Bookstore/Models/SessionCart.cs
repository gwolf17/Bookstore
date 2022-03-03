using Bookstore.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class SessionCart : Cart  //Inherits from the Cart model
    {
        //Method to create a new cart or use the current session cart
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;  //Create an instance of an ISession

            //Create an instance of this class and grab whatever is in there. If there is a SessionCart, use it, otherwise, create a new one
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            cart.Session = session;  //Set the cart Session object equal to the session info passed in

            return cart;
        }


        [JsonIgnore]
        public ISession Session { get; set; }  //Create an instance of a session

        //Override AddItem method
        public override void AddBook (Book b, int qty)
        {
            base.AddBook(b, qty);
            Session.SetJson("Cart", this);
        }

        //Override RemoveBook Method
        public override void RemoveBook(Book b)
        {
            base.RemoveBook(b);
            Session.SetJson("Cart", this);
        }

        //Override ClearCart Method
        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }
    }
}
