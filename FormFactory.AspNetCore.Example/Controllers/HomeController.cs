using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using FormFactory.Attributes;
using FormFactory.Example.Models.Examples;

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
        public virtual ActionResult SignInWithModel( SignInModel model)
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
            var ex = typeof(NestedFormsExample);
            var exampleTypes = ex.GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.Namespace == ex.Namespace && t.Name.Contains("Example"))
                .OrderBy(t => t.Name);
            return View("Index", new IndexModel() { exampleTypes = exampleTypes });

        }

        public class IndexModel
        {
            public IOrderedEnumerable<Type> exampleTypes { get; set; }
        }


        [HttpPost]
        public ActionResult Save()
        {
            return RedirectToAction("Index");
        }


        public JsonResult CountrySuggestions(string query)
        {
            return Json(GetCountries().Where(c => c.StartsWith(query ?? Guid.NewGuid().ToString(), StringComparison.CurrentCultureIgnoreCase)).ToArray());
        }
        static IEnumerable<string> GetCountries()
        {
            return new[] {"UK", "US", "Candada"};
        }
    }

    public class SignInModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}