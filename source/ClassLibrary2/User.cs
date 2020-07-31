using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Fam { get; set; }
        public string Name { get; set; }
        public string Otch { get; set; }
        public DateTime BirthDate { get; set; }
        public bool sex { get; set; }
        public string email { get; set; }
        public long phone { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }
    }
}
