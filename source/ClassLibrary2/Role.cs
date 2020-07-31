using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SysName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Func> Funcs { get; set; }
        public Role()
        {
            Users = new List<User>();
            Funcs = new List<Func>();
        }
    }
}
