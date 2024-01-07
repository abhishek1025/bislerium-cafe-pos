namespace bislerium_cafe_pos.Models
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNum { get; set; }
        public string EmployeeUserName { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; }
        public double OrderTotalAmount { get; set; }
        public double DiscountAmount { get; set; } = 0;
    }
}
