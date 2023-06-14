using Microsoft.AspNetCore.Mvc;

namespace project_is.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
