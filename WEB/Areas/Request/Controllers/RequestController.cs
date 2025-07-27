using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Request.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
