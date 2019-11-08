using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmarterCars.Models;
using SmarterCars.Services;

namespace SmarterCars.Controllers
{
    public class AdminController : Controller
    {
		private readonly ICustomerScheduleService _cusSchService;
		public AdminController(ICustomerScheduleService customerScheduleService)
		{
			_cusSchService = customerScheduleService;
		}
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

		// GET: Admin
		public ActionResult ShowAllInspection()
		{
			return View(_cusSchService.GetAllSchedules());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmInspection(int id)
		{
			try
			{
				if (_cusSchService.HasInspected(id))
				{
					return RedirectToAction("ShowAllInspection");
				}
				else
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to confirm inspection" });

				}
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Unable to confirm inspection" });
			}
			
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteInspection(int id)
		{
			try
			{
				if (_cusSchService.DeleteSchedule(id))
				{
					return RedirectToAction("ShowAllInspection");
				}
				else
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to delete inspection" });

				}
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Unable to delete inspection" });
			}

		}

		// GET: Admin/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}