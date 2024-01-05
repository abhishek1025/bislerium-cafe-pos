using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Models
{
    public class Order
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid CustomerID { get; set; }
        public String EmployeeUserName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderTime { get; set; }
        public Customer Customer { get; set; }
         
    }
}
