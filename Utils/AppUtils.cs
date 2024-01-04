using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Utils
{
    internal class AppUtils
    {

        public static string GetDesktopDirectoryPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "users.json");
        }

        public static string GetCofeeListFilePath()
        {
            return Path.Combine(GetDesktopDirectoryPath(), "coffeeList.json");
        }
    }
}
