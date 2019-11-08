using SmarterCars.DTOs;
using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.ViewModels
{
	public class HomeViewModel
	{

		public List<CarViewForHome> CarHomeViews { get; set; } 
		public List<EmitContentViewModel> Contents { get; set; }
	}

	public class CategoryViewModel
	{

		public List<CarViewForHome> CarHomeViews { get; set; }
		public Category Category { get; set; }
	}

	public class CarViewForHome
	{
		public int CarId { get; set; }
		public string ModelNumber { get; set; }
		public bool Available { get; set; }
		public List<string> Colors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		public double Amount { get; set; }
		public bool InspectionInProgress { get; set; }
		public List<CarImageViewModel> CarImages { get; set; }
		public List<CustomerScheduleDto> CustomerScheduleDtos { get; set; }
	}
}
