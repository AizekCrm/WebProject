using Microsoft.EntityFrameworkCore;
using WebProject.Models.CartModel;
using WebProject.Models.ItemsModel;
using WebProject.Models.OrderModel;
using WebProject.Models.UserModel;
using WebProject.Models.UserModel.Role;

namespace WebProject.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> Roles { get; set; }
        public DbSet<Cart> Cart { get; set; }


        public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            RoleUser adminRole = new RoleUser { Id = 1, Name = "admin" };
            RoleUser userRole = new RoleUser { Id = 2, Name = "user" };
            User adminUser = new User { 
                Id = 1, 
                Login = "adminAizek@mail.ru", 
                Password = "adminAizek02011997", 
                RoleId = adminRole.Id};

            builder.Entity<RoleUser>().HasData(new RoleUser[] { adminRole, userRole });
            builder.Entity<User>().HasData(new User[] { adminUser});

            base.OnModelCreating(builder);
        }
    }
}
