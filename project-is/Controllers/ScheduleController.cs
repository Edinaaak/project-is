using Microsoft.AspNetCore.Mvc;

namespace project_is.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
