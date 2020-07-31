using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DronTaxiWeb.Helpers
{
    public class Simple
    {
        public Simple(IConfiguration _config) {
            this.config = _config;
        }
        IConfiguration config;
        public string md5(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
        public string passHash(string pass)
        {
            return md5(pass + config.GetSection("Configuration")["HashSult"]);
        }
    }
}
