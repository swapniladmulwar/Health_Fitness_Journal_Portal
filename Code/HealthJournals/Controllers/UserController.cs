using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health.Controllers
{
    public class UserController : Controller
    {
        private readonly IHealthBALOperation _healthBALOperation;

        public UserController(IHealthBALOperation healthBALOperation)
        {
            _healthBALOperation = healthBALOperation ?? throw new ArgumentNullException(nameof(healthBALOperation));
        }

        // GET: UserController
        public ActionResult Index()
        {
            var userName = User.Claims.First(c => c.Type == "email").Value;
            ViewData["SubscribedFiles"] = _healthBALOperation.GetSubscribedHealthJournals(userName).Result;
            ViewData["Searched"] = new List<FileBAL>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string sortOrder, string searchString)
        {
            var userName = User.Claims.First(c => c.Type == "email").Value;
            ViewData["SubscribedFiles"] = _healthBALOperation.GetSubscribedHealthJournals(userName).Result;
            ViewData["Searched"] = _healthBALOperation.GetHealthJournalsBySearchKey(searchString).Result;
            return View();
        }

        // GET: UserController/Subscribtion/5
        public RedirectToActionResult Subscribtion(int id)
        {
            var userName = User.Claims.First(c => c.Type == "email").Value;
            var result =_healthBALOperation.AddSubscription(id.ToString(), userName).Result;
            return RedirectToAction("Index", "User");
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var data = _healthBALOperation.GetFileAsync(id.ToString()).Result;
            Response.Headers.Add("content-disposition", "inline; filename=filename.pdf");
            return new FileStreamResult(new MemoryStream(data.ToArray()), "application/pdf");
        }

        // GET: UserController/Delete/5
        public RedirectToActionResult UnSubscription(int id)
        {
            var removed = _healthBALOperation.RemoveSubscription(id.ToString());
            return RedirectToAction("Index", "User");
        }        
    }
}
