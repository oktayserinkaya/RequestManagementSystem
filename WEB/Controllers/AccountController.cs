using CORE.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using WEB.Models.ViewModels.AccountVM;

namespace WEB.Controllers
{
   
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private List<FakeUser> _users = new List<FakeUser>
    {
        new FakeUser
        {
            FirstName = "Ahmet",
            LastName = "Yılmaz",
            DepartmentName = "Satın Alma",
            Email = "ahmet@example.com",
            Password = "1234"
        },
        new FakeUser
        {
            FirstName = "Ayşe",
            LastName = "Kaya",
            DepartmentName = "İK",
            Email = "ayse@example.com",
            Password = "1234"
        }
    };

        [HttpGet]
        public IActionResult Login()
        {
            return View(); // → Views/Account/Login.cshtml beklenir
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen geçerli bilgiler giriniz.";
                return View(model);
            }

            // Basit kullanıcı kontrolü (gerçek DB'den alınabilir)
            if (model.Email == "admin@test.com" && model.Password == "1234")
            {
                HttpContext.Session.SetString("FirstName", "Admin");
                HttpContext.Session.SetString("LastName", "Test");
                HttpContext.Session.SetString("Department", "Yönetim");

                return RedirectToAction("Index", "Home");

            }

            TempData["Error"] = "Geçersiz e-posta veya şifre!";
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
