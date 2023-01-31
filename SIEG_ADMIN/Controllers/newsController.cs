using Microsoft.AspNetCore.Mvc;

namespace SIEG_ADMIN.Controllers
{
    public class newsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
