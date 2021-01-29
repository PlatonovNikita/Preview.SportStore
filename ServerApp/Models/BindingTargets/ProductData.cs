using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class ProductDataBase
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
        [Range(1, int.MaxValue, ErrorMessage = "Price must be at least 1")]
        public decimal Price
        {
            get => Product.Price;
            set => Product.Price = value;
        }
        
        [Required]
        public long? CategoryId
        {
            get => Product.CategoryId; 
            set => Product.CategoryId = value;
        }
        
        public bool? InStock
        {
            get => Product.InStock;
            set => Product.InStock = value;
        }

        [MaxLength(5000)]
        public string Description
        {
            get => Product.Description.Value;
            set => Product.Description.Value = value;
        }
         
        public Product Product { get; set; } = new Product() {};
    }

    public class ProductData : ProductDataBase
    {
        public ProductData()
        {
            Product = new Product {InStock = true, Description = new Description("")};
        }
    }
}