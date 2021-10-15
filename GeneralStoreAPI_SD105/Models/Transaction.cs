using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI_SD105.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemCount { get; set; }
        public DateTime DateOfTransaction { get; set; }
        //Foreign Keys
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }
        //Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}