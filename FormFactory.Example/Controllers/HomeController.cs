using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using FormFactory.Attributes;
using FormFactory.Example.Models;

namespace FormFactory.Example.Controllers
{
    public class HomeController : Controller
    {
        private Person Person { get { return Session["person"] as Person; } set { Session["person"] = value; } }
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            Person = Person ?? new Person(DateTime.Parse("22 Dec 1981"), "Fishing,Fighting".Split(',')) { Name = "Harry" };
            base.OnActionExecuting(context);
        }

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
            return View("Index", Person);
        }

        

        [HttpPost]
        public ActionResult Save()
        {
            this.UpdateModel(Person);
            return RedirectToAction("Index");
        }

    }

    public class SignInModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}