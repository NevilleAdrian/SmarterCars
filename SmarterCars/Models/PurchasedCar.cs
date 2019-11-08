using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class PurchasedCar
	{
		public int PurchasedCarId { get; set; }
		[ForeignKey("UserId")]
		public ApplicationUser UserWhoPurchased { get; set; }
		public string UserId { get; set; }
	}
}