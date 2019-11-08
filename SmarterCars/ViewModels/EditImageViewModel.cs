using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.ViewModels
{
	public class EditImageViewModel
	{
		public int CarImageId { get; set; }
		public string ImageToRemove { get; set; }
		public IFormFile File { get; set; }
		public string Description { get; set; }
		public string View { get; set; }
		public string _RequestVerificationToken { get; set; }
	}

	public class AddImageViewModel
	{
		public int CarId { get; set; }
		public IFormFile File { get; set; }
		public string Description { get; set; }
		public string View { get; set; }
		public string _RequestVerificationToken { get; set; }
	}
}
