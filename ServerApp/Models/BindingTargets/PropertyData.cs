using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class PropertyDataBase
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(0,2)]
        public int PropertyType { get; set; }
        
        public virtual Property Property => new Property
        {
            Name = Name, PropType = (PropertyType) PropertyType,
        };
    }
    public class PropertyData : PropertyDataBase
    {
        [Required]
        public long GroupPropertyId { get; set; }

        public override Property Property => new Property
        {
            Name = Name, PropType = (PropertyType) PropertyType,
            GroupPropertyId = GroupPropertyId
        };
    }
}