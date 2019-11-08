using SmarterCars.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class Promo
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PromoId { get; set; }
		[Required]
		public string PromoName { get; set; }
		[Required]
		public PromoType PromoType { get; set; }
		[Required]
		public double PromoValue { get; set; }

		[ForeignKey("CarId")]
		public Car Car { get; set; }
		public int CarId { get; set; }
	}
}