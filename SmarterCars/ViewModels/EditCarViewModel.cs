using SmarterCars.Models;
using System.Collections.Generic;


namespace SmarterCars.ViewModels
{
	public class EditCarViewModel
	{
		public int CarId { get; set; }
		public string ModelNumber { get; set; }
		public bool Available { get; set; }
		public List<Color> Colors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		public double Amount { get; set; }
		public List<CarImageViewModel> CarImages { get; set; }

	}

	public class EditCarReturnViewModel
	{
		public string ModelNumber { get; set; }
		public bool Available { get; set; }
		public List<int> ColorIds { get; set; }
		public List<string> OriginalColorNames { get; set; }
		public List<string> NewColors { get; set; }
		public List<string> Colors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		public double Amount { get; set; }
	}

}
