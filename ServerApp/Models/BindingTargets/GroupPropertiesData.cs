using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class GroupPropertiesData
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public long CategoryId { get; set; }

        public GroupProperty GroupProperty => new GroupProperty
        {
            Name = Name, CategoryId = CategoryId
        };
    }
}