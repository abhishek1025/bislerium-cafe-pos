using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Utils
{
    internal class AppUtils
    {

        public static string GetAppDataDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bislerium-cafe-pos-data");
        }

        public static string GetAppUsersFilePath()
        {
            return Path.Combine(GetAppDataDirectory(), "users.json");
        }
    }
}
