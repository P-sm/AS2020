using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Func
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SysName { get; set; }
        public ICollection<Role> Roles { get; set; }
        public Func()
        {
            Roles = new List<Role>();
        }
    }
}
