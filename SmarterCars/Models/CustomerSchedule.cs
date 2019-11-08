using SmarterCars.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Models
{
	public class CustomerSchedule
	{
		[Key]
		public int CustomerScheduleId { get; set; }
		public DateTime ScheduledTime { get; set; }
		public InspectionState State { get; set; }
		[ForeignKey("UserId")]
		public ApplicationUser UserWhoScheduled { get; set; }
		public string UserId { get; set; }
		[ForeignKey("CarId")]
		public Car Car { get; set; }
		public int CarId { get; set; }
	}
}
