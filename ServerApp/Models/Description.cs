namespace ServerApp.Models
{
    public class Description
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public long ProductId { get; set; }

        public Description(string value)
            => Value = value;
    }
}