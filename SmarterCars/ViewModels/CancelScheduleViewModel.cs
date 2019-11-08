using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.ViewModels
{
	public class CancelScheduleViewModel
	{
		public int Id { get; set; }
		public string __RequestVerificationToken { get; set; }
	}

	public class EditInspectionViewModel
	{
		public int InspectionId { get; set; }
		public DateTime EditedScheduleDate { get; set; }
		public bool CancelSchedule { get; set; }
		public string __RequestVerificationToken { get; set; }
	}
}
