using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Purchase
    {
        [Key]
        [BindNever]
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<BookLineItem> Lines { get; set; }  //Collection of BookLineItems called Lines

        [Required(ErrorMessage = "Name field cannot be blank")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address field cannot be blank")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        [Required(ErrorMessage = "City field cannot be blank")]
        public string City { get; set; }
        [Required(ErrorMessage = "State field cannot be blank")]
        public string State { get; set; }
        [Required(ErrorMessage = "Zip Code field cannot be blank")]
        public string Zip { get; set; }
        [Required(ErrorMessage = "Country field cannot be blank")]
        public string Country { get; set; }
        public bool Gift { get; set; }  //Is this order a gift?
    }
}
