using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Models
{
	public class Car
	{
		[Key]
		public int CarId { get; set; }
		[Required]
		public string ModelNumber { get; set; }
		[Required]
		public bool Available { get; set; }
		public ICollection<Color> AvailableColors { get; set; }
		public bool HasDefect { get; set; }
		public string DefectDescription { get; set; }
		[Required]
		public double Amount { get; set; }
		public ICollection<Promo> AvailablePromos { get; set; }
		public bool InspectionInProgress { get; set; }
		public ICollection<CarImage> CarImages { get; set; }
		public ICollection<CustomerSchedule> CustomerSchedules { get; set; }
	}
}
