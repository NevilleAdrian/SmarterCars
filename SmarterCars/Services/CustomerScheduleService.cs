using SmarterCars.Data;
using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public class CustomerScheduleService : ICustomerScheduleService
	{
		private readonly ApplicationDbContext _ctx;
		public CustomerScheduleService(ApplicationDbContext context)
		{
			_ctx = context;
		}


		public List<CustomerSchedule> GetAllSchedules() => 
		
			_ctx.CustomerSchedules.Include(c => c.UserWhoScheduled).Include(c => c.Car).ToList() ?? new List<CustomerSchedule>();
		

		public List<CustomerScheduleDto> GetAllSchedulesForAUser(string UserId) =>

			_ctx.CustomerSchedules.Include(c => c.UserWhoScheduled).Where(c => c.UserId == UserId).Select(c => new CustomerScheduleDto
			{
				CarId = c.CarId,
				CustomerScheduleId = c.CustomerScheduleId,
				UserId = c.UserId,
				ScheduledTime = c.ScheduledTime,
				State = c.State
			}).ToList() ?? new List<CustomerScheduleDto>();

		public bool BookSchedule(CustomerScheduleDto customerSchedule)
		{
			try
			{
				CustomerSchedule customerToSchedule = new CustomerSchedule
				{
					CarId = customerSchedule.CarId,
					ScheduledTime = customerSchedule.ScheduledTime,
					UserId = customerSchedule.UserId,
					State = Enums.InspectionState.WillInspect
				};
				_ctx.CustomerSchedules.Add(customerToSchedule);
				_ctx.SaveChanges();
				return true;
			}
			catch {
				return false;
			}
		}

		public bool EditInspection(EditInspectionViewModel editInspection)
		{
			if (CustomerScheduleExists(editInspection.InspectionId))
			{
				if (editInspection.EditedScheduleDate != null)
				{
					var scheduleOfInterest = _ctx.CustomerSchedules.Where(c => c.CustomerScheduleId == editInspection.InspectionId).SingleOrDefault();
					scheduleOfInterest.ScheduledTime = editInspection.EditedScheduleDate;
					scheduleOfInterest.State = editInspection.CancelSchedule ? Enums.InspectionState.Canceled : scheduleOfInterest.State;

					try
					{
						_ctx.Update(scheduleOfInterest);
						_ctx.SaveChanges();
						return true;
					}
					catch { }
				}


			}
			return false;
		}

		public bool CancelInspection(int id)
		{
			if (CustomerScheduleExists(id))
			{
				CustomerSchedule scheduleOfInterest = _ctx.CustomerSchedules.Where(cS => cS.CustomerScheduleId == id).SingleOrDefault();
				scheduleOfInterest.State = Enums.InspectionState.Canceled;
				try
				{
					_ctx.Update(scheduleOfInterest);
					_ctx.SaveChanges();
					return true;
				}
				catch
				{
					return false;
				}
			}
			return false;
		}

		public bool DeleteSchedule(int id)
		{
			if (CustomerScheduleExists(id))
			{
				CustomerSchedule scheduleOfInterest = _ctx.CustomerSchedules.Where(cS => cS.CustomerScheduleId == id).SingleOrDefault();
				try
				{
					_ctx.Remove(scheduleOfInterest);
					_ctx.SaveChanges();
					return true;
				}
				catch
				{
					return false;
				}
			}
			return false;
		}

		public bool HasInspected(int id)
		{
			if (CustomerScheduleExists(id))
			{
				CustomerSchedule scheduleOfInterest = _ctx.CustomerSchedules.Where(cS => cS.CustomerScheduleId == id).SingleOrDefault();
				scheduleOfInterest.State = Enums.InspectionState.Inspected;
				try
				{
					_ctx.Update(scheduleOfInterest);
					_ctx.SaveChanges();
					return true;
				}
				catch
				{
					return false;
				}
			}
			return false;
		}

		public bool CustomerScheduleExists(int id) => _ctx.CustomerSchedules.Any(cS => cS.CustomerScheduleId == id);
	}
}
