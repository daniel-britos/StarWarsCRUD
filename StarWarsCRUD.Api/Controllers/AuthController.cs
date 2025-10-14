using Microsoft.AspNetCore.Mvc;

namespace StarWarsCRUD.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
