using Microsoft.AspNetCore.Mvc;

namespace project_is.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
