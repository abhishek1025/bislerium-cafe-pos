using System;
using System.Text.Json;
using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;

namespace bislerium_cafe_pos.Services
{
	public class CofeeServices
	{

        // Creating a list of Coffee objects with prices in Nepalese Rupees (NPR)
        private List<Coffee> _coffeeList = new List<Coffee>
        {
            new Coffee { CoffeeType = "Cappuccino", Price = 150.0 },
            new Coffee { CoffeeType = "Latte", Price = 170.0 },
            new Coffee { CoffeeType = "Espresso", Price = 120.0 },
            new Coffee { CoffeeType = "Americano", Price = 140.0 },
            new Coffee { CoffeeType = "Mocha", Price = 180.0 },
            new Coffee { CoffeeType = "Macchiato", Price = 160.0 },
            new Coffee { CoffeeType = "Flat White", Price = 160.0 },
            new Coffee { CoffeeType = "Affogato", Price = 200.0 },
            new Coffee { CoffeeType = "Irish Coffee", Price = 190.0 },
            new Coffee { CoffeeType = "Turkish Coffee", Price = 130.0 },
            new Coffee { CoffeeType = "Ristretto", Price = 110.0 }
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

            if(coffeeList.Count == 0)
            {
                SaveCoffeeListInJsonFile(_coffeeList);
            }
		}
	}
}

