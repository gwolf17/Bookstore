using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo; //Link to repository

        //Constructor
        public HomeController(IBookstoreRepository temp)
        {
            repo = temp;  //Set equal to repo (class variable)
        }

        public IActionResult Index(int pageNum)
        {
            int pageSize = 10;  //# of books per page

            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)  //Order by Book Title
                .Skip(pageSize * (pageNum - 1))  //Skip the books shown on previous paages
                .Take(pageSize),  //Only show 10 books at a time

                //Create a new instance of the PageInfo class
                PageInfo = new PageInfo
                {
                    TotalBooks = repo.Books.Count(),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }
    }
}
