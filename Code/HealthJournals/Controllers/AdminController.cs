using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HealthBAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Health.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHealthBALOperation _healthBALOperation;

        public AdminController(IHealthBALOperation healthBALOperation)
        {
            _healthBALOperation = healthBALOperation ?? throw new ArgumentNullException(nameof(healthBALOperation));
        }
        // GET: AdminController
        public ActionResult Index()
        {
            var userName = User.Claims.First(c => c.Type == "email").Value;
            var files = _healthBALOperation.GetHealthJournals(userName).Result;
            return View(files);
        }


        [HttpPost]
        public RedirectToActionResult Index(IFormFile files)
        {
            var userName = User.Claims.First(c => c.Type == "email").Value;
            var isSuccess =_healthBALOperation.InsertHealthJournals(files, userName).Result;
            if (!isSuccess)
            {
                TempData["ErrorMessage"] = "Please upload only PDF files.";
            }
            return RedirectToAction("Index", "Admin");
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            var data = _healthBALOperation.GetFileAsync(id.ToString()).Result;            
            Response.Headers.Add("content-disposition", "inline; filename=filename.pdf");
            return new FileStreamResult(new MemoryStream(data.ToArray()), "application/pdf");
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
