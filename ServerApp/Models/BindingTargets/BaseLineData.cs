using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class BaseLineData
    {
        [Required]
        public long PropertyId { get; set; }
        
        [Required]
        public long GroupValuesId { get; set; }
    }
}