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
    public class CustomerService
    {
        public List<Customer> GetCustomerListFromJsonFile()
        {
            string customersFilePath = AppUtils.GetAppUsersFilePath();

            if (!File.Exists(customersFilePath))
            {
                return new List<Customer>();
            }

            var json = File.ReadAllText(customersFilePath);

            return JsonSerializer.Deserialize<List<Customer>>(json);
            
        }
        public Customer GetCustomerByPhoneNum(string customerPhoneNum)
        {
            List<Customer> customers = GetCustomerListFromJsonFile();
            Customer customer = customers.FirstOrDefault(c => c.CustomerPhoneNum == customerPhoneNum);
            return customer;
        }

        public void SaveCustomerListToJsonFile(List<Customer> customers)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string customerListFilePath = AppUtils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(customers);

            File.WriteAllText(customerListFilePath, json);
        }

        public void AddCustomer(Customer _customer)
        {
           
            Customer customer = GetCustomerByPhoneNum(_customer.CustomerPhoneNum);

            if(customer != null)
            {
                throw new Exception("Customer Doesn't exists");
            }

            List<Customer> customers = GetCustomerListFromJsonFile();

            customers.Add(_customer);

            SaveCustomerListToJsonFile(customers);
        }
    }
}
