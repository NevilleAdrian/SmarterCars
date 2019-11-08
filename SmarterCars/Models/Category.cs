using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }
		[Required]
		public string CategoryName { get; set; }
		public ICollection<Content> Contents { get; set; }
	}
}
