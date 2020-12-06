using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class BoolLineData : BaseLineData
    {
        [Required]
        public bool Value { get; set; }
        
        public BoolLine BoolLine => new BoolLine
        {
            Value = Value, PropertyId = PropertyId,
            //GroupValuesId = GroupValuesId
        };
    }
}