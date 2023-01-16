using Microsoft.AspNetCore.Mvc;

namespace SIEG.Controllers
{
    public class forum_postController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
