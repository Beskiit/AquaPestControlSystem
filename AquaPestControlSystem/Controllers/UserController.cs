using Microsoft.AspNetCore.Mvc;

namespace AquaPestControlSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult UserMakeAppointment()
        {
            return View();
        }

        public IActionResult LandingPage()
        {
            return View();
        }
        public IActionResult UserLogin()
        {
            var users = "airone github";
            return View();
        }
        public IActionResult UserCreateAccount()
        {
            return View();
        }
        public IActionResult UserForgotPassword()
        {
            return View();
        }
        public IActionResult UserServiceAnts()
        {
            return View();
        }
        public IActionResult UserServiceRoach()
        {
            return View();
        }
        public IActionResult UserServiceRodent()
        {
            return View();
        }
        public IActionResult UserServiceMosq()
        {
            return View();
        }
        public IActionResult UserServiceTermites()
        {
            return View();
        }
    }
}
