namespace bislerium_cafe_pos.Models
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNum { get; set; }
        public String EmployeeUserName { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
        //public int Month { get; set; } = DateTime.Now.Month;
        public List<OrderItem> OrderItems { get; set; }
        public Double OrderTotalAmount { get; set; }
    }
}
