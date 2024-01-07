using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class CustomerServices
    {
        // Retrieves the list of customers from the JSON file.
        public List<Customer> GetCustomerListFromJsonFile()
        {
            string customersFilePath = AppUtils.GetCustomersListFilePath();

            if (!File.Exists(customersFilePath))
            {
                return new List<Customer>();
            }

            var json = File.ReadAllText(customersFilePath);

            return JsonSerializer.Deserialize<List<Customer>>(json);

        }

        // Saves a list of customers to the JSON file.
        public void SaveCustomerListInJsonFile(List<Customer> customers)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string customerListFilePath = AppUtils.GetCustomersListFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(customers);

            File.WriteAllText(customerListFilePath, json);
        }

        // Retrieves a customer by their phone number from the JSON file
        public Customer GetCustomerByPhoneNum(string customerPhoneNum)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            return customer;
        }

        // Adds a new customer to the list and updates JSON file.
        public void AddCustomer(Customer _customer)
        {

            Customer isCustomerExists = GetCustomerByPhoneNum(_customer.CustomerPhoneNum);

            if (isCustomerExists != null)
            {
                throw new Exception("Customer Already exists");
            }

            List<Customer> customers = GetCustomerListFromJsonFile();
            customers.Add(_customer);

            SaveCustomerListInJsonFile(customers);
        }

        // Updates a customer's order count and saves the updated list to the JSON file.
        // This method is called when a customer places an order.
        public void UpdateCustomerOrderCout(string customerPhoneNum)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);

            customer.OrderCount++;

            SaveCustomerListInJsonFile(customers);
        }
    }
}
