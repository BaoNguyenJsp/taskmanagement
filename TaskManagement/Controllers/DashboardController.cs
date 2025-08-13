using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
