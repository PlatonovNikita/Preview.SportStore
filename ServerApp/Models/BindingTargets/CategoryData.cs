using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Models.BindingTargets
{
    public class CategoryData
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Za-z]+$")]
        public string NikName { get; set; }

        public Category Category => new Category
        {
            Name = Name,
            NikName = NikName
        };
    }
}