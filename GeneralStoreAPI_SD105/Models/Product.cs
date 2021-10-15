using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI_SD105.Models
{
    //public enum StockLevel { OutOfStock, LowStock, AdequateStock, HighStock, OverStock }
    public class Product
    {
        //SKU
        [Key]
        public string SKU { get; set; }
        //Name
        [Required]
        public string Name { get; set; }
        //Price
        [Required]
        public decimal Price { get; set; }
        //Description
        public string Description { get; set; }
        //Quantity in stock
        [Required]
        public int QuantityInStock { get; set; }
        //InStock
        public bool IsInStock
        {
            get
            {
                return QuantityInStock > 0;
            }
        }
    }
}