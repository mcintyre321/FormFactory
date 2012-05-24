using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FormFactory.Attributes;

namespace FormFactory.Example.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public virtual ActionResult SignIn(string email, [DataType(DataType.Password)] string password)
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

        [HttpPost]
        public virtual ActionResult SignInWithModel([FormModel] SignInModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || model.Password != "password")
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

    public class SignInModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}