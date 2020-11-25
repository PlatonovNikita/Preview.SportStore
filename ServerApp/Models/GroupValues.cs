using System.Collections;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public class GroupValues
    {
        public long Id { get; set; }
        
        public long? GroupPropertyId { get; set; }
        public GroupProperty GroupProperty { get; set; }
        
        public long ProductId { get; set; }
        
        public IEnumerable<DoubleLine> DoubleProps { get; set; }
        public IEnumerable<BoolLine> BoolProps { get; set; }
        public IEnumerable<StrLine> StrProps { get; set; }
    }
}