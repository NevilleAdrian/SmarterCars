using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class Color
	{
		[Key]
		public int ColorId { get; set; }
		[Required]
		public string ColorName { get; set; }

		[ForeignKey("CarId")]
		public Car Car { get; set; }
		public int CarId { get; set; }

	}
}