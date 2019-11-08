using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmarterCars.Models;
using SmarterCars.Services;
using SmarterCars.ViewModels;

namespace SmarterCars.Controllers
{
    public class CategoriesController : Controller
    {
		private readonly ICategoryService _catService;
		private readonly ICarService _carService;

		public CategoriesController(ICategoryService categoryService, ICarService carService)
		{
			_catService = categoryService;
			_carService = carService;
		}
		
		// GET: Categories
		public async Task<IActionResult> Index()
        {
            return View(await _catService.GetAllCategories(includeContents: false));
        }

        // GET: Categories/Details/5
        public async Task<ActionResult> Details(int id)
        {
			Category categoryInfo = await _catService.GetCategoryByID(id, includeContents: true);
			List<CarViewForHome> carInfo = _carService.GetAllCarViewsForHome();
			CategoryViewModel viewModel = new CategoryViewModel {
				Category = categoryInfo,
				CarHomeViews = carInfo
			};

			return View(viewModel);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
			if (ModelState.IsValid)
			{
				try
				{
					// TODO: Add insert logic here
					if(_catService.AddCategory(category))
						return RedirectToAction(nameof(Index));
					else
						return View("Error", new ErrorViewModel { RequestId = "Unable to add category" });

				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to add category" });
				}
			}
			return View();
		}

        // GET: Categories/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _catService.GetCategoryByID(id, includeContents: false));
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
			if (ModelState.IsValid)
			{
				try
				{
					if (await _catService.UpdateCategoryWithID(id, category))
					{
						return RedirectToAction(nameof(Index));
					}
					else
					{
						return View("Error", new ErrorViewModel { RequestId = "Unable to update category" });

					}
				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "Unable to update category" });

				}
			}
			return RedirectToAction("Edit");
            
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _catService.GetCategoryByID(id, includeContents: false));
        }

        // POST: Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
				var result = await _catService.DeleteCategoryWithID(id);
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
				return View("Error", new ErrorViewModel { RequestId = "Something bad happened" });

			}
		}
    }
}