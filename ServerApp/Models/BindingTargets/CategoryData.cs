using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class CategoryData
    {
        [Required]
        public string Name { get; set; }

        public Category Category => new Category
        {
            Name = Name
        };
    }
}