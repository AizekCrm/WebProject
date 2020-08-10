using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebProject.Models.UserModel.Role;

namespace WebProject.Models.UserModel
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserContactPhone { get; set; }


        public int? RoleId { get; set; }
        public RoleUser Role { get; set; }
    }
}
