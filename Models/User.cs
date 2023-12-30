using bislerium_cafe_pos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Models
{
    public class User
    { 
        public string UserName {get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        public bool HasInitialPasswordChanged { get; set; } = false;
    }
}
