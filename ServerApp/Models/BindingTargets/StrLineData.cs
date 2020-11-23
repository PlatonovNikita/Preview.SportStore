using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class StrLineData : BaseLineData
    {
        [Required]
        public string Value { get; set; }

        public StrLine StrLine => new StrLine
        {
            Value = Value, PropertyId = PropertyId,
            GroupValuesId = GroupValuesId
        };
    }
}