using Microsoft.AspNetCore.Mvc;

namespace SIEG.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [ActionName("product_info")]
        public IActionResult xxx()
        {
            return View();
        }
        public IActionResult sell()
        {
            return View();
        }

        public IActionResult order()
        {
            return View();
        }

    }
}
