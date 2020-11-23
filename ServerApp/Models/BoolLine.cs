namespace ServerApp.Models
{
    public class BoolLine
    {
        public long Id { get; set; }
        public bool Value { get; set; }
        
        public long PropertyId { get; set; }
        public Property Property { get; set; }
        
        public long GroupValuesId { get; set; }
    }
}