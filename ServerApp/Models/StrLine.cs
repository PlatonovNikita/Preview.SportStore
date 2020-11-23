namespace ServerApp.Models
{
    public class StrLine
    {
        public long Id { get; set; }
        public string Value { get; set; }
        
        public long PropertyId { get; set; } 
        public Property Property { get; set; }
        
        public long GroupValuesId { get; set; }
    }
}