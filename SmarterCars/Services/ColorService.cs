using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmarterCars.Data;
using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public class ColorService : IColorService
	{
		public readonly ApplicationDbContext _ctx;
		private readonly ILogger<ColorService> _logger; 
		public ColorService(ApplicationDbContext context, ILogger<ColorService> logger)
		{
			_ctx = context;
			_logger = logger;
		}

		public void AddColor(int carId, ICollection<string> colors)
		{
			IEnumerable<Color> color = colors.Select(c => new Color
			{
				ColorName = c,
				CarId = carId
			});
			try
			{
				_ctx.Colors.AddRange(color);
				_ctx.SaveChanges();
			}
			catch (DbUpdateException ex)
			{
				_logger.LogInformation($"Could not create the colors because of {ex.Message} ");
			}
			
			
		}

		public void EditColor(EditCarReturnViewModel editCarReturn, int carId)
		{
			List<Color> colorsToAdd = new List<Color>();
			List<Color> colorsToUpdate = new List<Color>();
			List<Color> colorsToDelete = new List<Color>();
			if (editCarReturn.ColorIds != null)
			{
				for (int i = 0; i < editCarReturn.ColorIds.Count; i++)
				{
					if (ColorExists(editCarReturn.ColorIds[i]))
					{
						if (editCarReturn.OriginalColorNames[i] != editCarReturn.NewColors[i])
						{
							if (string.IsNullOrEmpty(editCarReturn.NewColors[i]))
							{
								colorsToDelete.Add(new Color
								{
									CarId = carId,
									ColorId = editCarReturn.ColorIds[i],
									ColorName = editCarReturn.OriginalColorNames[i]
								});
							}
							else
							{
								colorsToUpdate.Add(new Color
								{
									CarId = carId,
									ColorId = editCarReturn.ColorIds[i],
									ColorName = editCarReturn.NewColors[i]
								});
							}

						}
					}

				}
			}
			
			if (editCarReturn.Colors != null)
			{
				foreach (string color in editCarReturn.Colors)
				{
					if (!string.IsNullOrEmpty(color))
					{
						colorsToAdd.Add(new Color
						{
							CarId = carId,
							ColorName = color
						});
					}
				}
			}
			
			if (colorsToDelete.Count > 0)
			{
				try
				{ _ctx.Colors.RemoveRange(colorsToDelete);
				} catch { }
				
			}
			if (colorsToUpdate.Count > 0)
			{
				try
				{
					_ctx.Colors.UpdateRange(colorsToUpdate);
				} catch { }
				
			}
			if (colorsToAdd.Count > 0)
			{
				try
				{
					_ctx.Colors.AddRange(colorsToAdd);
					_ctx.SaveChanges();
				} catch { }
				
			}
		}

		public void DeleteColor(ICollection<Color> color)
		{
			if (color != null)
			{
				try
				{
					_ctx.Colors.RemoveRange(color);
					_ctx.SaveChanges();
				}
				catch { }
			}
		}

		public bool ColorExists(int id) => _ctx.Colors.Any(c => c.ColorId == id);
	}
}
