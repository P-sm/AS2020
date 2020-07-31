using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using DronTaxiWeb.Models;
using DronTaxiWeb.Helpers;
using DronTaxiWeb.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Data.Entity;

namespace DronTaxiWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        string cs;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            cs = _configuration.GetSection("Configuration")["ConnectionString"];
        }
        [Authorize(Roles = "admin, operator,client")]
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();
            var userLogin = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            model.Text += "userLogin=" + userLogin + "<br>";
            using (DatabaseContext db = new DatabaseContext(cs))
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == userLogin);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    model.userInfo = user;
                    model.userInfo.Password = "";
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
