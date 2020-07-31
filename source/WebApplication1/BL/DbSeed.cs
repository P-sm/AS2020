using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DronTaxiWeb.Helpers;

namespace DronTaxiWeb.BL
{
    public class DbSeed
    {
        public DbSeed(IConfiguration _config)
        {
            this.config = _config;
            this.simpleHelper = new Simple(config);
            this.cs = config.GetSection("Configuration")["ConnectionString"];
            addRoles();
            addUsers();
        }
        IConfiguration config;
        string cs;
        Simple simpleHelper;
        public void addRoles()
        {
            using (DatabaseContext db = new DatabaseContext(cs))
            {
                if (!db.Roles.Any(x => x.SysName == "admin"))
                {
                    var item1 = new Role
                    {
                        SysName = "admin",
                        Name = "Администратор",
                    };
                    var item2 = new Role
                    {
                        SysName = "operator",
                        Name = "Оператор",
                    };
                    var item3 = new Role
                    {
                        SysName = "client",
                        Name = "Клиент",
                    };
                    db.Roles.Add(item1);
                    db.Roles.Add(item2);
                    db.Roles.Add(item3);
                }
                db.SaveChanges();
            }
        }
        public void addUsers()
        {
            using (DatabaseContext db = new DatabaseContext(cs))
            {
                if (!db.Users.Any(x => x.Login == "admin"))
                {
                    User user1 = new User
                    {
                        Login = "admin",
                        Name = "Василий",
                        Fam = "Булкин",
                        Otch = "Петрович",
                        email = "admin@admin.ru",
                        BirthDate = new DateTime(1981, 01, 01),
                        phone = 99999999999,
                        Roles = new List<Role>(){db.Roles.First(x => x.SysName == "Admin")},
                        Password = simpleHelper.passHash("123")
                    };
                    db.Users.Add(user1);
                }
                db.SaveChanges();
            }
        }

    }
}
