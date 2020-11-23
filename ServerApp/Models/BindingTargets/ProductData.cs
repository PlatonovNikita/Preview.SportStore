using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class ProductData
    {

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string Name
        {
            get => Product.Name; 
            set => Product.Name = value;
        }

        [Required]
        [MaxLength(5000)]
        public string Description
        {
            get => Product.Description; 
            set => Product.Description = value;
        }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be at least 1")]
        public decimal Price
        {
            get => Product.Price;
            set => Product.Price = value;
        }

        public bool InStock
        {
            get => Product.InStock;
            set => Product.InStock = value;
        }

        [Required]
        public long? CategoryId
        {
            get => Product.CategoryId; 
            set => Product.CategoryId = value;
        }
         
        public Product Product { get; set; } = new Product() {InStock = true};
    }
}