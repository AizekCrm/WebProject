using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject.Models.UserModel.Role
{
    public class RoleUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> RoleUsers { get; set; }
        public RoleUser()
        {
            RoleUsers = new List<User>();
        }

    }
}
