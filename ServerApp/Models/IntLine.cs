namespace ServerApp.Models
{
    public class IntLine
    {
        public long Id { get; set; }
        public int Value { get; set; }
        
        public long? PropertyId { get; set; }
        public Property Property { get; set; }
        
        public long GroupValuesId { get; set; }
    }
}