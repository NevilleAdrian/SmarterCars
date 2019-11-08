using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmarterCars.Data;
using SmarterCars.Models;
using SmarterCars.Services;
using SmarterCars.ViewModels;

namespace SmarterCars.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _ctx;
		private readonly IContentService _conService; 
		private readonly ICarService _carService; 

		public HomeController(ApplicationDbContext context, IContentService service, ICarService carService)
		{
			_conService = service;
			_ctx = context;
			_carService = carService;
		}
		public async Task<IActionResult> Index()
		{
			var content = await _conService.GetAllContents(true) ?? new List<EmitContentViewModel>();
			HomeViewModel viewModel = new HomeViewModel {
				Contents = content.Where(con => con.ShowOnHome).ToList(),
				CarHomeViews = _carService.GetAllCarViewsForHome() ?? new List<CarViewForHome>()
			};
			return View(viewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
