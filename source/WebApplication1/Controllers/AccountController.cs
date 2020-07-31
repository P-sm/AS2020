
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DAL;
using DronTaxiWeb.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.Entity;
using DronTaxiWeb.Helpers;
using System.Linq;
using DronTaxiWeb.BL;

namespace DronTaxiWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration config;
        string cs;
        Simple simpleHelper;
        public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            config = configuration;
            this.simpleHelper = new Simple(config);
            cs = config.GetSection("Configuration")["ConnectionString"];
        }
        /*private ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }*/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db = new DatabaseContext(cs))
                {
                    User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Email);
                    if (user == null)
                    {
                        // добавляем пользователя в бд
                        user = new User { email = model.Email, Password = model.Password };
                        Role userRole = await db.Roles.FirstOrDefaultAsync(r => r.SysName == "client");
                        if (userRole != null)
                            user.Roles = new List<Role> { userRole };

                        db.Users.Add(user);
                        await db.SaveChangesAsync();

                        await Authenticate(user); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            var ds = new DbSeed(config);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (DatabaseContext db = new DatabaseContext(cs))
                {
                    var passHash = simpleHelper.passHash(model.Password);
                    var allu = db.Users;
                    User user = await db.Users
                        .Include(u => u.Roles)
                        .FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == passHash);
                    if (user != null)
                    {
                        await Authenticate(user); // аутентификация

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Roles.FirstOrDefault().SysName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login", "Account");
        }
    }
}
