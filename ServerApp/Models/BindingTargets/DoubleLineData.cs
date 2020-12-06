using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class DoubleLineData : BaseLineData
    {
        [Required]
        public double Value { get; set; }

        public DoubleLine DoubleLine => new DoubleLine
        {
            Value = Value, PropertyId = PropertyId,
            //GroupValuesId = GroupValuesId
        };
    }
}