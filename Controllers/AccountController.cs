using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ClimateSmartAgriculture.Data;
using ClimateSmartAgriculture.Models;
using ClimateSmartAgriculture.Services;
using System.Diagnostics;

namespace ClimateSmartAgriculture.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly WeatherService _weatherService;

        public AccountController(UserService userService, ApplicationDbContext context, WeatherService weatherService)
        {
            _userService = userService;
            _context = context;
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                if (_userService.Register(model.Name, model.Email, model.Password, "Farmer"))
                    return RedirectToAction("Login");

                ModelState.AddModelError("", "Email is already registered.");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _userService.Authenticate(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            var user = _userService.GetUserByEmail(userEmail);
            var crops = await _context.Crops.Where(c => c.Farm.UserId == user.UserId).ToListAsync();
            //var weatherData = await _weatherService.GetWeatherDataAsync("DefaultLocation");

            var viewModel = new DashboardViewModel
            {
                Crops = crops,
            //    WeatherData = weatherData
            };

            ViewBag.UserEmail = userEmail;
            return View(viewModel);
            //return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
