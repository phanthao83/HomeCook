using HC.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HC.DataAccess.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DBInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            if (_db.Roles.Any(r => r.Name == UserType.AdminRole)) return;

            //Create Role 
            _roleManager.CreateAsync(new IdentityRole(UserType.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserType.SupplierRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserType.CustomerRole)).GetAwaiter().GetResult();


            //Create Admin user
            _ = _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = false,
                Name = "Thao Phan"

            }, "P@ssword123").GetAwaiter().GetResult();

            ApplicationUser user = _db.AppUser.Where(u => u.Email == "admin@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, UserType.AdminRole).GetAwaiter().GetResult();

        }
    }
}
