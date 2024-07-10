using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        
        [Required]
        public string ProductName { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        
        // public ICollection<CartItem> CartItems { get; set; }
        
        // public ICollection<SalesItem> SalesItems { get; set; }
    }
}
