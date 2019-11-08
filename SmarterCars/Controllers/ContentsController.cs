using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmarterCars.Data;
using SmarterCars.Models;
using SmarterCars.Services;
using SmarterCars.ViewModels;

namespace SmarterCars.Controllers
{
    public class ContentsController : Controller
    {
		private readonly IContentService _conService;
		private readonly ICarService _carService;
		private readonly IHostingEnvironment _env;
		private readonly ApplicationDbContext _ctx;
		public ContentsController(IContentService conService, ApplicationDbContext context, ICarService carService, IHostingEnvironment env)
		{
			_conService = conService;
			_env = env;
			_ctx = context;
			_carService = carService;
		}
		// GET: Contents
		public async Task<IActionResult> Index()
        {
            return View(await _conService.GetAllContents(includeComments: true));
        }

        // GET: Contents/Details/5
        public async Task<ActionResult> Details(int id)
        {
			EmitContentViewModel content = await _conService.GetContentByID(id, true);
			List<CarViewForHome> carInfo = _carService.GetAllCarViewsForHome();
			ContentPageViewModel viewModel = new ContentPageViewModel
			{
				CarViews = carInfo,
				Content = content
			};
			return View(viewModel);
        }

        // GET: Contents/Create
        public ActionResult Create()
        {
			ViewData["Category"] = new SelectList(_ctx.Categories, "CategoryId", "CategoryName");
			return View();

		}

		// POST: Contents/Create
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AddContentViewModel addContent)
        {
			if (ModelState.IsValid)
			{
				try
				{
					if (_conService.AddContent(addContent, _env))
					{
						return RedirectToAction(nameof(Index));
					}
					
					return View("Error", new ErrorViewModel { RequestId = "Unable to perform your request" });
					
				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to perform your request" });
				}
			}
			return View();
        }

        // GET: Contents/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
			var model = await _conService.GetContentByID(id, includeComments: false);
			ViewData["Category"] = new SelectList(_ctx.Categories, "CategoryId", "CategoryName", model.CategoryId);
			return View(model);
        }

        // POST: Contents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EditContentViewModel editContent)
        {
			if (ModelState.IsValid)
			{
				try
				{
					if (await _conService.UpdateContentWithID(id, editContent, _env))
					{
						return RedirectToAction(nameof(Index));
					}
					// TODO: Add update logic here

					return View("Error", new ErrorViewModel { RequestId = "Unable to edit" });
				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to edit" });

				}
			}
			return RedirectToAction(nameof(Edit));
            
        }

        // GET: Contents/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _conService.GetContentByID(id, includeComments: false));
        }

        // POST: Contents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DeleteContentViewModel deleteContent)
        {
			if (ModelState.IsValid)
			{
				try
				{
					var result = await _conService.DeleteContentWithID(deleteContent, _env);
					if (result.Item1)
					{
						return RedirectToAction(nameof(Index));
					}
					else if (result.Item2 == "500")
					{
						return View("Error", new ErrorViewModel { RequestId = "There was a server error" });

					}
					else
					{
						return NotFound();

					}
				}
				catch
				{
					return View();
				}
			}
			return View();
            
        }
    }
}