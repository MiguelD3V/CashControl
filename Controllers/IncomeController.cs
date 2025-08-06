using Microsoft.AspNetCore.Mvc;

namespace Cashcontrol.API.Controllers
{
    public class IncomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
