
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
    public class UserServices
    {
        private static List<User> _seedUsersList = new()
        {
            new User()
            {
                UserName = "administrator",
                Password = "administrator",
                Role = Role.Administrator,
            },

            new User()
            {
                UserName = "staff",
                Password = "staff",
                Role = Role.Staff
            }
        };



        public static void SaveAllUsersInJsonFile(List<User> users)
        {
            string appDataDirPath = AppUtils.GetAppDataDirectory();
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(users);

            File.WriteAllText(appUsersFilePath, json);
        }

         public static List<User> GetAllUsersFromJsonFile()
        {
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!File.Exists(appUsersFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<User>>(json);
        }
        

        public static void SeedUsers()
        {
            var users = GetAllUsersFromJsonFile();

            if(users.Count == 0)
            {
                SaveAllUsersInJsonFile(_seedUsersList);
            }
        }
       
        public static User LogIn(string userName, string password, string role) 
        {
            const string errorMessage = "Invalid username or password";

            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password is required");
            }

            List<User> users = GetAllUsersFromJsonFile();

            User user = users.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.Role.ToString() == role);

            return user ?? throw new Exception(errorMessage);
        }

    }
}