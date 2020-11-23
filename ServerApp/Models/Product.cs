using System.Collections;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; } = true;
        
        public long? CategoryId { get; set; }
        public Category Category { get; set; }
        
        public IEnumerable<GroupValues> GroupsValues { get; set; } 
    }
}