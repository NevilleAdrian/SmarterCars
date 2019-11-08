using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.Services;
using SmarterCars.ViewModels;

namespace SmarterCars.Controllers
{
	public class CarsController : Controller
	{
		private readonly ICarService _carService; 
		private ICarImageService _carImgService; 
		public CarsController(ICarService carService, ICarImageService carImageService)
		{
			_carService = carService;
			_carImgService = carImageService;
		}
        // GET: Cars
        public ActionResult Index()
        {
			List<CarViewModel> carViewModels = _carService.GetAllCars();
            return View(carViewModels);
        }

        // GET: Cars/Details/5
        public ActionResult Details(int id)
        {
			EditCarViewModel carOfInterest = _carService.GetCarById(id);
			if (carOfInterest != null)
			{
				return View(carOfInterest);
			}
			return NotFound();
		}

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarDto carDto)
        {
			if (ModelState.IsValid)
			{
				try
				{
					if (_carService.AddCar(carDto))
					{
						return RedirectToAction(nameof(Index));
					}
				}
				catch
				{
					return View();
				}
			}
			return View();
            
        }

		//POST: Cars/CarImageToAdd
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CarImageToAdd(AddImageViewModel addImageView)
		{
			if (ModelState.IsValid)
			{
				try {
					bool added = _carImgService.AddCarImage(addImageView);
					if (added)
					{
						return Ok(new { Value = "New image added" });
					}
					return Ok(new { Value = "Image was not added, try again." });
				}
				catch {
					return View("Error", new ErrorViewModel { RequestId = "A database error occured" });
				}
			}
			return View(nameof(Create));
		}

        // GET: Cars/Edit/5
        public ActionResult Edit(int id)
        {
			EditCarViewModel carOfInterest = _carService.GetCarById(id);
			if (carOfInterest != null)
			{
				return View(carOfInterest);
			}
			return NotFound();

        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditCarReturnViewModel editCarReturn)
        {
			if (ModelState.IsValid)
			{
				try
				{
					bool updated = _carService.UpdateCarWithId(id, editCarReturn);
					if (updated)
					{
						return RedirectToAction(nameof(Index));
					}
					

				}
				catch
				{
					return View("Error", new ErrorViewModel { RequestId = "There was an error" });
				}
			}
			return View();
            
        }

		// POST: Cars/EditCarImage
		[HttpPost]
		public IActionResult EditCarImage(EditImageViewModel editImageView)
		{
			if (ModelState.IsValid)
			{
				try
				{
					string imagePath = _carImgService.EditCarImage(editImageView);
					if (!string.IsNullOrEmpty(imagePath))
					{
						return Ok(new { Path = imagePath });
					}
					return Ok(new { Path = "" });

				}
				catch
				{
					return View();
				}
			}
			return View();
		}

		// GET: Cars/Delete/5
		public ActionResult Delete(int id)
        {
			EditCarViewModel car = _carService.GetCarById(id);
			if (car == null)
			{
				return NotFound();
			}
			return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCar(int id)
        {
			
			try
			{
				_carService.DeleteCar(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Could not delete" });
			}
        }

		// POST: Cars/DeleteCarImage
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteCarImage(EditImageViewModel editImageView)
		{
			if (ModelState.IsValid)
			{
				try
				{
					bool imageRemoved = _carImgService.DeleteCarImage(editImageView);
					if (imageRemoved)
					{
						return Ok(new { Value = "Record and image deleted" });
					}
					return Ok(new { Value = "Record deleted" });

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