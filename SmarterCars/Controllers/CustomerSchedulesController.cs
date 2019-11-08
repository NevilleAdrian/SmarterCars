using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmarterCars.Data;
using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.Services;
using SmarterCars.ViewModels;

namespace SmarterCars.Controllers
{
    public class CustomerSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerScheduleService _cusSchService;

        public CustomerSchedulesController(ApplicationDbContext context, ICustomerScheduleService customerSchedule)
        {
            _context = context;
			_cusSchService = customerSchedule;
        }

        // GET: CustomerSchedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerSchedules.Include(c => c.UserWhoScheduled);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CustomerSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSchedule = await _context.CustomerSchedules
                .Include(c => c.UserWhoScheduled)
                .FirstOrDefaultAsync(m => m.CustomerScheduleId == id);
            if (customerSchedule == null)
            {
                return NotFound();
            }

            return View(customerSchedule);
        }

        // GET: CustomerSchedules/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: CustomerSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerScheduleId,ScheduledTime,UserId")] CustomerSchedule customerSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", customerSchedule.UserId);
            return View(customerSchedule);
        }

		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateSchedule(CustomerScheduleDto customerSchedule)
		{
			if (ModelState.IsValid)
			{
				try {
					if (_cusSchService.BookSchedule(customerSchedule))
					{
						return Ok(new { value = "Booked" });
					}
					return Ok(new { value = "Error" });
				}
				catch {
					return View("Error", new ErrorViewModel { RequestId = "Could not add schedule" });
				}
			}
			return View("Error", new ErrorViewModel { RequestId = "This is not supposed to happen" });
		}

		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CancelSchedule(CancelScheduleViewModel cancelSchedule)
		{
			try
			{
				if (_cusSchService.CancelInspection(cancelSchedule.Id))
				{
					return Ok(new { value = "Schedule canceled" });
				}
				return Ok(new { value = "Error" });
			}
			catch
			{
				return View("Error", new ErrorViewModel { RequestId = "Could not add schedule" });
			}
			
		}

		// GET: CustomerSchedules/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSchedule = await _context.CustomerSchedules.Where(c => c.CustomerScheduleId == id && c.State == Enums.InspectionState.WillInspect).Select(c => new EditInspectionViewModel {
				EditedScheduleDate = c.ScheduledTime,
				InspectionId = c.CustomerScheduleId,
				CancelSchedule = false
			}).SingleOrDefaultAsync();
            if (customerSchedule == null)
            {
                return NotFound();
            }
			return View(customerSchedule);
        }

        // POST: CustomerSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditInspectionViewModel customerSchedule)
        {
            if (id != customerSchedule.InspectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					if (_cusSchService.EditInspection(customerSchedule))
					{
						return RedirectToAction("Index", "Home");
					}
					else
					{
						return NotFound();
					}
                }
                catch
                {
					return View("Error", new ErrorViewModel { RequestId = "Could not update please try again" });
                }
            }
            
            return View(customerSchedule);
        }

        // GET: CustomerSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerSchedule = await _context.CustomerSchedules
                .Include(c => c.UserWhoScheduled)
                .FirstOrDefaultAsync(m => m.CustomerScheduleId == id);
            if (customerSchedule == null)
            {
                return NotFound();
            }

            return View(customerSchedule);
        }

        // POST: CustomerSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerSchedule = await _context.CustomerSchedules.FindAsync(id);
            _context.CustomerSchedules.Remove(customerSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerScheduleExists(int id)
        {
            return _context.CustomerSchedules.Any(e => e.CustomerScheduleId == id);
        }
    }
}
