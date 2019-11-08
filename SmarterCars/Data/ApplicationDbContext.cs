using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmarterCars.Models;

namespace SmarterCars.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Car> Cars { get; set; }
		public DbSet<CarImage> CarImages { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Color> Colors { get; set; }
		public DbSet<Content> Contents { get; set; }
		public DbSet<CustomerSchedule> CustomerSchedules { get; set; }
		public DbSet<Promo> Promos { get; set; }
		public DbSet<PurchasedCar> PurchasedCars { get; set; }
		public DbSet<Comment> Comments { get; set; }
	}
}
