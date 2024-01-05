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
    public class AddInItemsServices
    {
        // Creating a list of AddIns objects with proper names and prices in NPR
        private readonly List<AddInItem> _addInItemsList = new()
        {
            new() { Name = "Extra Sugar", Price = 10.0 },
            new() { Name = "Whipped Cream", Price = 15.0 },
            new() { Name = "Chocolate Syrup", Price = 12.0 },
            new() { Name = "Vanilla Extract", Price = 18.0 },
            new() { Name = "Caramel Drizzle", Price = 22.0 },
            new() { Name = "Hazelnut Flavor", Price = 20.0 },
            new() { Name = "Cinnamon Powder", Price = 17.0 },
            new() { Name = "Almond Milk", Price = 25.0 },
            new() { Name = "Whiskey Shot", Price = 30.0 },
            new() { Name = "Special Syrup Blend", Price = 28.0 }
        };


        public void SaveAddInItemsListInJsonFile(List<AddInItem> addInItemList)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string addInItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(addInItemList);

            File.WriteAllText(addInItemsListListFilePath, json);
        }
        public List<AddInItem> GetAddInItemsListListFromJsonFile()
        {
            string addInsItemsListListFilePath = AppUtils.GetAddInItemsListFilePath();

            if (!File.Exists(addInsItemsListListFilePath))
            {
                return new List<AddInItem>();
            }

            var json = File.ReadAllText(addInsItemsListListFilePath);

            return JsonSerializer.Deserialize<List<AddInItem>>(json);
        }


        public void SeedAddInItemsList()
        {
            List<AddInItem> addInsList = GetAddInItemsListListFromJsonFile();

            if (addInsList.Count == 0)
            {
                SaveAddInItemsListInJsonFile(_addInItemsList);
            }
        }

        public AddInItem GetAddInItemDetailsByID(String addInItemID)
        {
            List<AddInItem> coffeeList = GetAddInItemsListListFromJsonFile();
            AddInItem addInItem = coffeeList.FirstOrDefault(addIn => addIn.Id.ToString() == addInItemID);
            return addInItem;
        }


        public void UpdateAddInItemDetails(AddInItem addInItem)
        {
            List<AddInItem> addInItemsList = GetAddInItemsListListFromJsonFile();

            AddInItem addInItemToUpdate = addInItemsList.FirstOrDefault(_addInItem => _addInItem.Id.ToString() == addInItem.Id.ToString());

            if (addInItemToUpdate == null)
            {
                throw new Exception("Add-In item not found");
            }

            addInItemToUpdate.Name = addInItem.Name;
            addInItemToUpdate.Price = addInItem.Price;

            SaveAddInItemsListInJsonFile(addInItemsList);
        }
    }
}
