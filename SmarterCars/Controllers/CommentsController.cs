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
    public class CommentsController : Controller
    {
		private readonly ICommentService _comService;
		public CommentsController(ICommentService service)
		{
			_comService = service;
		}
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _comService.GetAllComments());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
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

		// POST: Comments/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateComment(Comment comment)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (_comService.AddComment(comment))
					{
						return Ok(new { Value = "Success" });
					}
					return Ok(new { Value = "Error" });
				}
				catch
				{
					return Ok(new { Value = "Error" });
				}
			}
			return Ok(new { Value = "Error" });
			
		}

		// GET: Comments/Edit/5
		public async Task<IActionResult> Edit(int id)
        {
            return View(await _comService.GetCommentByID(id));
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Comment comment)
        {
			if (ModelState.IsValid)
			{
				try
				{
					if (await _comService.UpdateCommentWithID(id, comment))
					{
						return RedirectToAction(nameof(Index));
					}
					return View("Error", new ErrorViewModel { RequestId = "Cannot update" });

				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "Cannot update" });
				}
			}
			return View();
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _comService.GetCommentByID(id));
        }

        // POST: Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
				var status = await _comService.DeleteCommentWithID(id);

				if (status.Item1)
				{
					return RedirectToAction(nameof(Index));
				}
				else if (status.Item2 == "500")
				{
					return View("Error", new ErrorViewModel { RequestId = "There was an error during operation" });

				}
				return NotFound();

			}
            catch
            {
				return View("Error", new ErrorViewModel { RequestId = "There was an error during operation" });

			}
		}
    }
}