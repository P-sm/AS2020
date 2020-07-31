using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronTaxiWeb.Models
{
    public class HomeViewModel
    {
        //public HomeViewModel(){}

        public HomeViewModel()
        {
            
        }
        public User userInfo;
        public string Text { get; set; } = "";
    }
}
