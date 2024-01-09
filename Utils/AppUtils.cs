using System.Text.RegularExpressions;

namespace bislerium_cafe_pos.Utils
{
    internal class AppUtils
    {
        public static string GetDesktopDirectoryPath()
        {
            string dbFileDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "BisleriumCafeDB");
            
            if (!Directory.Exists(dbFileDirectory))
            {
                Directory.CreateDirectory(dbFileDirectory);
            }

            return dbFileDirectory;
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "users.json");
        }

        public static string GetCofeeListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "coffeeList.json");
        }

        public static string GetAddInItemsListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "addInsList.json");
        }

        public static string GetCustomersListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "customers.json");
        }

        public static string GetOrderItemListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orderItems.json");
        }

        public static string GetOrderListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "orders.json");
        }

        // Helper method to check if a string contains only numeric characters
        public static bool IsNumeric(string input)
        {
            Regex numericRegex = new Regex("^[0-9]+$");
            return numericRegex.IsMatch(input);
        }
    }
}
