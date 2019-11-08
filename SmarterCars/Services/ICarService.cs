using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.ViewModels;
using System.Collections.Generic;

namespace SmarterCars.Services
{
	public interface ICarService
	{
		bool AddCar(CarDto carDto);
		List<CarViewModel> GetAllCars();
		EditCarViewModel GetCarById(int id);
		bool UpdateCarWithId(int id, EditCarReturnViewModel editCarReturn);
		void DeleteCar(int id);
		List<CarViewForHome> GetAllCarViewsForHome();
	}
}
