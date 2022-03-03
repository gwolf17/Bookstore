using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFPurchaseRepository : IPurchaseRepository  //Inherits from IPurchaseRepository
    {
        public BookstoreContext context;  //Instance of our database

        //Constructor
        public EFPurchaseRepository (BookstoreContext temp)
        {
            context = temp;
        }

        //Get the purchase and the associated book
        public IQueryable<Purchase> Purchases => context.Purchases.Include(x => x.Lines).ThenInclude(x => x.Book);

        public IQueryable<Purchase> Purchase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SavePurchase (Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));  //Get the Book associated with each line

            if (purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);  //Add the purchase
            }

            context.SaveChanges();  //Save the changes
        }
    }
}
