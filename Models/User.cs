using bislerium_cafe_pos.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bislerium_cafe_pos.Models
{
    public class User
    {
        //[Required]
        //[StringLength(8, ErrorMessage = "Username is required")]
        public string UserName {get; set; }

        //[Required]
        //[StringLength(8, ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        //[Required]
        //[StringLength(8, ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool HasInitialPasswordChanged { get; set; } = false;
    }
}
