using SmarterCars.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.DTOs
{
	public class CustomerScheduleDto
	{
		public int CustomerScheduleId { get; set; }
		public DateTime ScheduledTime { get; set; }
		public string UserId { get; set; }
		public int CarId { get; set; }
		public InspectionState State { get; set; }
		public string __RequestVerificationToken { get; set; }
	}
}
