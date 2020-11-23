using System.Collections;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public class GroupProperty
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public long CategoryId { get; set; }
        
        public IEnumerable<Property> Properties { get; set; } 
    }
}