using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public interface ICustomerScheduleService
	{
		bool BookSchedule(CustomerScheduleDto customerSchedule);
		bool CancelInspection(int id);
		bool HasInspected(int id);
		bool EditInspection(EditInspectionViewModel editInspection);
		List<CustomerSchedule> GetAllSchedules();
		List<CustomerScheduleDto> GetAllSchedulesForAUser(string UserId);
		bool DeleteSchedule(int id);
	}
}
