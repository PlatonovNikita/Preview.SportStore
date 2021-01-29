using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.BindingTargets
{
    public class SearchLines
    {
        public IEnumerable<BoolLineSearch> BSearch;
        public IEnumerable<DoubleLineSearch> DSearch;
        public IEnumerable<StrLineSearch> StrSearch;
    }

    public class DoubleLineSearch 
    {
        public long PropertyId { get; set; }
        
        public double? Min { get; set; }
        public double? Max { get; set; }
    }

    public class BoolLineSearch
    {
        public long PropertyId { get; set; }
        
        public bool Value { get; set; }
    }

    public class StrLineSearch
    {
        public long PropertyId { get; set; }
        
        public IEnumerable<string> Strings { get; set; }
    }
}