using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Services
{
    public class OrderServices
    {
        public List<Order> GetOrdersFromJsonFile()
        {
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!File.Exists(orderListFilePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(orderListFilePath);

            return JsonSerializer.Deserialize<List<Order>>(json);
        }   

        public void PlaceOrder(Order order)
        {
            List<Order> orders = GetOrdersFromJsonFile();
            orders.Add(order);

            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string orderListFilePath = AppUtils.GetOrderListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(orders);

            File.WriteAllText(orderListFilePath, json);
        }
    }
}
