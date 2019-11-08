using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<CustomerSchedule> MySchedules { get; set; }
		public ICollection<PurchasedCar> MyCars { get; set; }
	}
}
