using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace FormFactory.Example.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public virtual ActionResult SignIn(string email, [DataType(DataType.Password)] string password, bool remember, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(email) || password != "password")
            {
                ModelState.AddModelError("email", "Incorrect login details");
            }

            if (ModelState.IsValid)
            {
                RedirectToAction("Index");
            }
            return View("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}