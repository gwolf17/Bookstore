using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalBooks { get; set; }
        public int BooksPerPage { get; set; }
        public int CurrentPage { get; set; }

        //Calculate total # of pages
        public int TotalPages => (int)Math.Ceiling((double)TotalBooks / BooksPerPage);
    }
}
