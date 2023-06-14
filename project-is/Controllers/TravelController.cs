using Microsoft.AspNetCore.Mvc;

namespace project_is.Controllers
{
    public class TravelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
