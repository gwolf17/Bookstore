using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public interface IPurchaseRepository
    {
        IQueryable<Purchase> Purchase { get; set; }  //

        void SavePurchase (Purchase purchase); //To save the info, we need to be passed a Purchase object
    }
}
