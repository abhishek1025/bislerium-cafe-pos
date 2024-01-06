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
      
        public void AddItemInOrderItemsList(List<OrderItem> _orderItems, Guid itemID, string itemName, String itemType, Double itemPrice)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.ItemID.ToString() == itemID.ToString() && x.ItemType == itemType);

            if (orderItem != null)
            {
                orderItem.Quantity++;
                orderItem.TotalPrice = orderItem.Quantity * itemPrice;
            }

            orderItem = new()
            {
                ItemID = itemID,
                ItemName = itemName,
                ItemType = itemType,
                Quantity = 1,
                Price = itemPrice,
                TotalPrice = itemPrice
            };

            _orderItems.Add(orderItem);


        }

        public void DeleteItemInOrderItemsList(List<OrderItem> _orderItems, Guid orderItemID)
        {
            OrderItem orderItem = _orderItems.FirstOrDefault(x => x.OrderItemID == orderItemID);

            if (orderItem != null)
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

        public double CalculateTotalAmount(IEnumerable<OrderItem> Elements)
        {
            double totalAmount = 0;

            foreach (var item in Elements)
            {
                totalAmount += item.TotalPrice;
            }
            return totalAmount;
        }
    }

    
}
