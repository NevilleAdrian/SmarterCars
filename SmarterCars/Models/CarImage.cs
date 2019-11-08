using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class CarImage
	{
		[Key]
		public int CarImageId { get; set; }
		[Required]
		[MaxLength(200, ErrorMessage = "Letters should be 200 or less")]
		public string Description { get; set; }
		[Required]
		[MaxLength(20, ErrorMessage = "Letters should be 20 or less")]
		public string View { get; set; }
		public string CarImagePath { get; set; }
		[ForeignKey("CarId")]
		public Car Car { get; set; }
		public int CarId { get; set; }
	}
}