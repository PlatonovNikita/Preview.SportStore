using System.Collections;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<GroupProperty> GroupProperties { get; set; }
    }
}