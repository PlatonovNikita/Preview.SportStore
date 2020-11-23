using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class IntLineData : BaseLineData
    {
        [Required]
        public int Value { get; set; }

        public IntLine IntLine => new IntLine
        {
            Value = Value, PropertyId = PropertyId,
            GroupValuesId = GroupValuesId
        };
    }
}