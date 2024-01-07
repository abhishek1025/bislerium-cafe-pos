namespace bislerium_cafe_pos.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhoneNum { get; set; }
        public int OrderCount { get; set; } = 1;
    }
}
