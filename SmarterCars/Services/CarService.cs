using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
	public class CarService : ICarService
	{
		private readonly ApplicationDbContext _ctx;
		private readonly IColorService _colorService;
		private readonly ICarImageService _carImgService;
		public CarService(ApplicationDbContext context, IColorService colorService, ICarImageService carImageService)
		{
			_ctx = context;
			_colorService = colorService;
			_carImgService = carImageService;
		}

		public bool AddCar(CarDto carDto)
		{
			//Car car = Mapper.Map<Car>(carDto);
			Car car = new Car {
				Amount = carDto.Amount,
				Available = carDto.Available,
				DefectDescription = carDto.DefectDescription,
				HasDefect = carDto.HasDefect,
				ModelNumber = carDto.ModelNumber
			};
			try
			{
				_ctx.Cars.Add(car);
				_ctx.SaveChanges();
			}
			catch (DbUpdateException)
			{
				return false;
			}
			if (carDto.Colors != null)
			{
				_colorService.AddColor(car.CarId, carDto.Colors);
			}
			if (carDto.CarImages != null)
			{
				List<CarImageDto> carImageDtos = new List<CarImageDto>();
				for (int i = 0; i < carDto.CarImages.Count; i++)
				{
					carImageDtos.Add(new CarImageDto {
						CarImage = carDto.CarImages.ElementAt(i),
						Description = carDto.Descriptions.ElementAt(i),
						View = carDto.Views.ElementAt(i)
					});
				}
				_carImgService.AddCarImages(car.CarId, carImageDtos);
			}
			return true;
		}

		public List<CarViewModel> GetAllCars() => _ctx.Cars.Select(c => new CarViewModel
		{
			CarId = c.CarId,
			Amount = c.Amount,
			Available = c.Available,
			CarImages = c.CarImages.Select(cI => new CarImageViewModel
			{
				CarImagePath = cI.CarImagePath,
				Description = cI.Description,
				View = cI.View
			}).ToList(),
			Colors = c.AvailableColors.Select(aC => aC.ColorName).ToList(),
			DefectDescription = c.DefectDescription,
			HasDefect = c.HasDefect,
			InspectionInProgress = c.InspectionInProgress,
			ModelNumber = c.ModelNumber
		}).ToList() ?? new List<CarViewModel>();

		public EditCarViewModel GetCarById(int id)
		{
			if (CarExists(id))
			{
				var carOfInterest = _ctx.Cars.Where(c => c.CarId == id)
					.Include(c => c.CarImages).Include(c => c.AvailableColors)
					.Select(c => new EditCarViewModel
					{
						CarId = c.CarId,
						Amount = c.Amount,
						Available = c.Available,
						CarImages = c.CarImages.Select(cI => new CarImageViewModel {
								CarImageId = cI.CarImageId,
								CarImagePath = cI.CarImagePath,
								Description = cI.Description,
								View = cI.View
							}).ToList(),
						Colors = c.AvailableColors.ToList(),
						DefectDescription = c.DefectDescription,
						HasDefect = c.HasDefect,
						ModelNumber = c.ModelNumber
					}).SingleOrDefault();
				return carOfInterest;
			}
			return null;
		}

		public bool UpdateCarWithId(int id, EditCarReturnViewModel editCarReturn)
		{
			if (CarExists(id))
			{
				//First we deal with colors
				_colorService.EditColor(editCarReturn, id);
				//Then we deal with the car
				Car carToUpdate = _ctx.Cars.Where(c => c.CarId == id).Select(c => new Car {
					Amount = editCarReturn.Amount,
					Available = editCarReturn.Available,
					CarId = id,
					DefectDescription = editCarReturn.DefectDescription,
					HasDefect = editCarReturn.HasDefect,
					InspectionInProgress = c.InspectionInProgress,
					ModelNumber = editCarReturn.ModelNumber

				}).SingleOrDefault();

				try
				{
					_ctx.Cars.Update(carToUpdate);
					_ctx.SaveChanges();
					return true;
				}
				catch {
					return false;
				}
			}

			return false;
			
		}
		public void DeleteCar(int id)
		{
			if (CarExists(id))
			{
				Car carToRemove = _ctx.Cars.Where(c => c.CarId == id).Include(c => c.AvailableColors).Include(c => c.CarImages).SingleOrDefault();
				try
				{
					_colorService.DeleteColor(carToRemove.AvailableColors);
					_carImgService.DeleteCarImage(carToRemove.CarImages);
					_ctx.Remove(carToRemove);
					_ctx.SaveChanges();
				}
				catch { }
				
			}
			
		}
		public bool CarExists(int id) => _ctx.Cars.Any(cId => cId.CarId == id);

		public List<CarViewForHome> GetAllCarViewsForHome() =>
			_ctx.Cars.Where(c => c.Available).Include(c => c.CarImages).Include(c => c.AvailableColors).Include(c => c.CustomerSchedules)
				.Select(c => new CarViewForHome
				{
					Amount = c.Amount,
					Available = c.Available,
					Colors = c.AvailableColors.Select(a => a.ColorName).ToList(),
					CarId = c.CarId,
					CarImages = c.CarImages.Select(i => new CarImageViewModel
					{
						CarImageId = i.CarImageId,
						CarImagePath = i.CarImagePath,
						Description = i.Description,
						View = i.View
					}).ToList(),
					DefectDescription = c.DefectDescription,
					HasDefect = c.HasDefect,
					ModelNumber = c.ModelNumber,
					CustomerScheduleDtos = c.CustomerSchedules.Select(cS => new DTOs.CustomerScheduleDto
					{
						UserId = cS.UserId,
						ScheduledTime = cS.ScheduledTime,
						State = cS.State,
						CustomerScheduleId = cS.CustomerScheduleId
					}).ToList(),

				}).ToList();
	}
}
