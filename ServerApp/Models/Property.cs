using System;
using System.Collections.Generic;

namespace ServerApp.Models
{
    public enum PropertyType
    {
        Bool = 0,
        Double = 1,
        Str = 2
    }
    public class Property
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PropertyType PropType { get; set; }
        
        public long GroupPropertyId { get; set; }
    }
}