using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
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
            IOrderedEnumerable<Tuple<Type, string>> exampleTypes = ex.GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.Namespace == ex.Namespace && t.Name.Contains("Example"))
                
                .Select(t => Tuple.Create(t, GetSourceForType(t)))
                .OrderBy(t => t.Item1.Name);

            return View("Index", new IndexModel() { exampleTypes = exampleTypes });

        }
        static Hashtable Cache = new Hashtable();
        private string GetSourceForType(Type type)
        {
            var html = Cache[type.Name + ".cs"] as string;
            if (html == null)
            {
                var address = "https://raw.github.com/mcintyre321/FormFactory/master/FormFactory.AspNetCore.Example/Models/Examples/" + type.Name + ".cs";
                try
                {
                    html = new HttpClient().GetAsync(address).Result.Content.ReadAsStringAsync().Result;
                    html = string.Join(Environment.NewLine, html.Split(Environment.NewLine.ToCharArray()).Where(line => line.Trim().StartsWith("using ") == false));
                    Cache[type.Name + ".cs"] = html;
                }
                catch (Exception)
                {
                    return "Could not get source from " + address;
                }
            }
            return html;
            
        }

        public class IndexModel
        {
            public IOrderedEnumerable<Tuple<Type, string>> exampleTypes { get; set; }
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