using Microsoft.AspNetCore.Http;
using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.ViewModels
{
	public class CarViewModel
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
		
	}

	public class CarImageViewModel
	{
		public int CarImageId { get; set; }
		public string Description { get; set; }
		public string View { get; set; }
		public string CarImagePath { get; set; }
	}

	

	public class CarReturnViewModel
	{
		public string ModelNumber { get; set; }
		public bool Available { get; set; }
		public List<string> Colors { get; set; }
		public List<string> EditedColors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		public double Amount { get; set; }
		public bool InspectionInProgress { get; set; }
		public ICollection<IFormFile> EditedCarImages { get; set; }
		public ICollection<string> EditedDescriptions { get; set; }
		public ICollection<string> EditedViews { get; set; }

	}
}
