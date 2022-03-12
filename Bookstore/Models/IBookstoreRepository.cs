using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface IBookstoreRepository
    {
        IQueryable<Book> Books { get; }

        //Set up CRUD Methods
        public void SaveBook(Book b);
        public void AddBook(Book b);
        public void DeleteBook(Book b);
    }
}
