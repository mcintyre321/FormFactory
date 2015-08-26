using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using FormFactory.Attributes;
using FormFactory.Example.Models;
using FormFactory.Example.Models.Examples;

namespace FormFactory.Example.Controllers
{
    public class HomeController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext context)
        {
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
            return View("Index");
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
            return from ri in
                       from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                       select new RegionInfo(ci.LCID)
                   group ri by ri.TwoLetterISORegionName
                   into g
                       //where g.Key.Length == 2
                   select g.First().DisplayName;
        }
    }

    public class SignInModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}