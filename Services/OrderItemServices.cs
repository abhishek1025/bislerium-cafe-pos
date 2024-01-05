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
    public class OrderItemServices
    {
        public List<OrderItem> GetOrderItemsFromJsonFile()
        {
            string orderItemsListFilePath = AppUtils.GetOrderItemListFilePath();

            if (!File.Exists(orderItemsListFilePath))
            {
                return new List<OrderItem>();
            }

            var json = File.ReadAllText(orderItemsListFilePath);

            return JsonSerializer.Deserialize<List<OrderItem>>(json);
        }

        public void SaveOrderItemsInJsonFile(List<OrderItem> _orderItems)
        {
            List<OrderItem> currentOrderItems = GetOrderItemsFromJsonFile();
            currentOrderItems.AddRange(_orderItems);

            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string orderItemsListFilePath = AppUtils.GetOrderItemListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(currentOrderItems);
            File.WriteAllText(orderItemsListFilePath, json);
        }

        public void AddItemInOrderItemsList(List<OrderItem> _orderItems, Guid itemID, String ItemType, Double ItemPrice)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.ItemID == itemID && x.ItemType == ItemType);

            if (orderItem != null)
            {
                orderItem.Quantity++;
                orderItem.TotalPrice = orderItem.Quantity * ItemPrice;
            }

            orderItem = new()
            {
                ItemID = itemID,
                ItemType = ItemType,
                Quantity = 1,
                Price = ItemPrice,
                TotalPrice = ItemPrice
            };

            _orderItems.Add(orderItem);


        }

        public void DeleteItemInOrderItemsList(List<OrderItem> _orderItems, Guid orderItemID)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

           if(orderItem != null)
            {
                _orderItems.Remove(orderItem);
            }   
        }

        public void ManageQuantityOfOrderItem(List<OrderItem> _orderItems, Guid orderItemID, String action)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
            {
                if (action == "add")
                {
                    orderItem.Quantity++;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
                else if (action == "subtract" && orderItem.Quantity > 1)
                {
                    orderItem.Quantity--;
                    orderItem.TotalPrice = orderItem.Quantity * orderItem.Price;
                }
            }
        }   
    }
}
