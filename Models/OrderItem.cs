namespace bislerium_cafe_pos.Models
{
    public class OrderItem
    {
        public Guid OrderItemID { get; set; } = Guid.NewGuid();
        public Guid ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

    }
}
