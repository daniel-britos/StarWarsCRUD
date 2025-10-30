using Microsoft.AspNetCore.Mvc;

namespace StarWarsCRUD.Api.Controllers
{
    public class NavesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
