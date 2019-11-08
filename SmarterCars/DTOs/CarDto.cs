using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.DTOs
{
	public class CarDto
	{
		public string ModelNumber { get; set; }
		public bool Available { get; set; }
		public ICollection<string> Colors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		public double Amount { get; set; }
		public ICollection<IFormFile> CarImages { get; set; }
		public ICollection<string> Descriptions { get; set; }
		public ICollection<string> Views { get; set; }

	}


	public class CarImageDto
	{
		public IFormFile CarImage { get; set; }
		public string Description { get; set; }
		public string View { get; set; }
	}
}
