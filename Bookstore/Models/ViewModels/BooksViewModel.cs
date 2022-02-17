using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class BooksViewModel
    {
        public IQueryable<Book> Books { get; set; }  //Create variable for the list of books to be returned
        public PageInfo PageInfo { get; set; }  //Instance of the PageInfo class
    }
}
