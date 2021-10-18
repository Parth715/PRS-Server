
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PRS_Server.Models
{
    public class PoLines
    {
        
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public PoLines(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.LineTotal = quantity * Product.Price;
        }
    }
}