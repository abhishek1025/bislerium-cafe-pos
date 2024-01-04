﻿
using bislerium_cafe_pos.Models;
using bislerium_cafe_pos.Utils;
using System.Data;
using System.Text.Json;

namespace bislerium_cafe_pos.Services
{
    public class UserServices
    {

        private List<User> _seedUsersList = new()
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



        public void SaveAllUsersInJsonFile(List<User> users)
        {
            string appDataDirPath = AppUtils.GetDesktopDirectoryPath();
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!Directory.Exists(appDataDirPath))
            {
                Directory.CreateDirectory(appDataDirPath);
            }

            var json = JsonSerializer.Serialize(users);

            File.WriteAllText(appUsersFilePath, json);
        }

         public List<User> GetAllUsersFromJsonFile()
        {
            string appUsersFilePath = AppUtils.GetAppUsersFilePath();

            if (!File.Exists(appUsersFilePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(appUsersFilePath);

            return JsonSerializer.Deserialize<List<User>>(json);
        }
        

        public void SeedUsers()
        {
            var users = GetAllUsersFromJsonFile();
            //SaveAllUsersInJsonFile(_seedUsersList);
            if (users.Count == 0)
            {
                SaveAllUsersInJsonFile(_seedUsersList);
            }
        }
       
        public User LogIn(string userName, string password, string role) 
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

        public User ChangePassword(User currentUser, string newPassword, string currentPassword)
        {
            

            List<User> users = GetAllUsersFromJsonFile();

            User user = users.FirstOrDefault(u => u.UserName == currentUser.UserName && u.Role.ToString() == currentUser.Role.ToString());

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            bool isCurrentPasswordValid = user.Password == currentPassword;

            if (!isCurrentPasswordValid)
            {
                throw new Exception("Incorrect Current password");
            };


            user.Password = newPassword;
            user.HasInitialPasswordChanged = true;

            SaveAllUsersInJsonFile(users);

            return user;            
        }
    }
}