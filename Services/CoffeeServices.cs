using System;
using System.Text.Json;
using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;

namespace bislerium_cafe_pos.Services
{
    public class CoffeeServices
    {

        // Creating a list of Coffee objects with prices in Nepalese Rupees (NPR)
        private readonly List<Coffee> _coffeeList = new()
        {
            new() { CoffeeType = "Cappuccino", Price = 150.0 },
            new() { CoffeeType = "Latte", Price = 170.0 },
            new() { CoffeeType = "Espresso", Price = 120.0 },
            new() { CoffeeType = "Americano", Price = 140.0 },
            new() { CoffeeType = "Mocha", Price = 180.0 },
            new() { CoffeeType = "Macchiato", Price = 160.0 },
            new() { CoffeeType = "Flat White", Price = 160.0 },
            new() { CoffeeType = "Affogato", Price = 200.0 },
            new() { CoffeeType = "Irish Coffee", Price = 190.0 },
            new() { CoffeeType = "Turkish Coffee", Price = 130.0 },
            new() { CoffeeType = "Ristretto", Price = 110.0 }
        };

        public void SaveCoffeeListInJsonFile(List<Coffee> coffeeList)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string coffeeListFilePath = AppUtils.GetCofeeListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(coffeeList);

            File.WriteAllText(coffeeListFilePath, json);
        }

        public List<Coffee> GetCoffeeListFromJsonFile()
        {
            string coffeeListFilePath = AppUtils.GetCofeeListFilePath();

            if (!File.Exists(coffeeListFilePath))
            {
                return new List<Coffee>();
            }

            var json = File.ReadAllText(coffeeListFilePath);

            return JsonSerializer.Deserialize<List<Coffee>>(json);
        }


        public void SeedCofeeDetails()
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            if (coffeeList.Count == 0)
            {
                SaveCoffeeListInJsonFile(_coffeeList);
            }
        }

        public Coffee GetCofeeDetailsByID(String coffeeID)
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();
            Coffee coffee = coffeeList.FirstOrDefault(coffee => coffee.Id.ToString() == coffeeID);
            return coffee;
        }

        public void UpdateCofeeDetails(Coffee coffee)
        {
            List<Coffee> coffeeList = GetCoffeeListFromJsonFile();

            Coffee coffeeToUpdate = coffeeList.FirstOrDefault(_coffee => _coffee.Id.ToString() == coffee.Id.ToString());

            if (coffeeToUpdate == null)
            {
                throw new Exception("Coffee not found");
            }

            coffeeToUpdate.CoffeeType = coffee.CoffeeType;
            coffeeToUpdate.Price = coffee.Price;

            SaveCoffeeListInJsonFile(coffeeList);
        }

    }
}

