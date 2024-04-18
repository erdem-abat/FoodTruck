using Microsoft.AspNetCore.Mvc;

namespace FoodTruck.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
