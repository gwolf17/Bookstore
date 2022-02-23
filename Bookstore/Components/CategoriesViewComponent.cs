using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Components
{
    public class CategoriesViewComponent : ViewComponent  //Inheret from ViewComponent class
    {
        //Create an instance of the repository
        private IBookstoreRepository repo { get; set; }

        //Constructor
        public CategoriesViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }

        //When this view component is invoked, return the following view along with the info
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["categoryType"];  //Get any category types being passed in

            //Pull in each category from our database and pass it to the view
            var categories = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
