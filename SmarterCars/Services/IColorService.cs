using SmarterCars.Models;
using SmarterCars.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public interface IColorService
	{
		void AddColor(int carId, ICollection<string> colors);
		void EditColor(EditCarReturnViewModel editCarReturn, int carId);
		void DeleteColor(ICollection<Color> color);
	}
}
