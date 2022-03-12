using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class EFShippingRepository : IShippingRepository  //Inheritance
    {
        //Instance of our database
        private BookstoreContext context;

        //Constructor
        public EFShippingRepository (BookstoreContext temp)
        {
            context = temp;
        }

        //Get the purchase and the associated book
        public IQueryable<Purchase> Purchases => context.Purchases.Include(x => x.Lines).ThenInclude(x => x.Book);

        //Override the SavePurchase method
        public void SavePurchase(Purchase purchase)
        {
            context.AttachRange(purchase.Lines.Select(x => x.Book));  //Get the book associated with the particular line

            //If there is no purchaseId, add to the Purchases model
            if (purchase.PurchaseId == 0)
            {
                context.Purchases.Add(purchase);
            }

            context.SaveChanges();
        }
    }
}
