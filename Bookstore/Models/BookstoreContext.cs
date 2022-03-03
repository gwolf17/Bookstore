using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bookstore.Models
{
    public class BookstoreContext : DbContext
    {
        //Constructor
        public BookstoreContext(DbContextOptions<BookstoreContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        //Add this line to connect to the Purchase model
        public DbSet<Purchase> Purchases { get; set; }
    }
}
