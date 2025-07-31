using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using ThePlayCastleMVC.Models;
using ThePlayCastleMVC.Services;

namespace ThePlayCastleMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HttpService client = new HttpService();
        string BaseUrl = @"https://localhost:44393/";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            ViewBag.User = new Users();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> SignUp(Users user)
        {
            user.UserType = "User";
            ApiResponse apiResponse = await client.PostAsync(BaseUrl + "api/users/create", user);
            if (apiResponse.Status == "Success")
            {
                return View("Login");
            }
            else
            {
                ViewBag.Error = apiResponse.Message;
                ViewBag.User = user;
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Login(string Email, string PWD)
        {

            ViewBag.Email = Email;
            ViewBag.PWD = PWD;


            Users user = new Users() { Email = Email, Pwd = PWD };

            var response = await client.PostAsync(BaseUrl + "api/login", user);
            if (response.Status == "Error")
            {
                ViewBag.Error = response.Message;
                return View();
            }
            else
            {
                user = JsonConvert.DeserializeObject<Users>(response.Data.ToString());
                HttpContext.Session.SetString("UID", user.Uid.ToString());
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserType", user.UserType);
            }


            return View("Search");
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UID");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserType");
            return View("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
