namespace ServerApp.Models
{
    public class DoubleLine
    {
        public long Id { get; set; }
        public double Value { get; set; }
        
        public long? PropertyId { get; set; }
        public Property Property { get; set; }
        
        public long GroupValuesId { get; set; }
    }
}