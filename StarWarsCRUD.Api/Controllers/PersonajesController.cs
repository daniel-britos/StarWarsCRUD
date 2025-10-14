using Microsoft.AspNetCore.Mvc;

namespace StarWarsCRUD.Api.Controllers
{
    public class PersonajesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
