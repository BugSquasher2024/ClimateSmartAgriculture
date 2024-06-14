using ClimateSmartAgriculture.Models;
using ClimateSmartAgriculture.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserService _userService;

    public HomeController(ILogger<HomeController> logger, UserService userService)
    {
        _logger = logger;
        _userService = userService;
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
    public async Task<IActionResult> Register(string name, string email, string password)
    {
        if (_userService.Register(name, email, password, "Farmer"))
            return RedirectToAction("Login");

        ModelState.AddModelError("", "Email is already registered.");
        return View();
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
            // Set user session
            HttpContext.Session.SetString("UserEmail", user.Email);
            return RedirectToAction("Dashboard");
        }

        ModelState.AddModelError("", "Invalid email or password.");
        return View();
    }

    public IActionResult Dashboard()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Login");
        }

        ViewBag.UserEmail = userEmail;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
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
