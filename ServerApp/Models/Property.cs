namespace ServerApp.Models
{
    public enum PropertyType
    {
        Bool = 0,
        Int = 1,
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