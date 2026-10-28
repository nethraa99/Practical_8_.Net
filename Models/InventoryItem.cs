using System.ComponentModel.DataAnnotations;

namespace Practical_8_.Net.Models
{
    public class InventoryItem
    {
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [Display(Name = "Product Name")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, 100000, ErrorMessage = "Quantity must be 0 or greater.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000.00, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}