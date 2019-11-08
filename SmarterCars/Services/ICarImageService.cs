using SmarterCars.DTOs;
using SmarterCars.Models;
using SmarterCars.ViewModels;
using System.Collections.Generic;

namespace SmarterCars.Services
{
	public interface ICarImageService
	{
		void AddCarImages(int carId, ICollection<CarImageDto> carImageDto);
		string EditCarImage(EditImageViewModel editImageView);
		bool DeleteCarImage(EditImageViewModel editImageView);
		bool AddCarImage(AddImageViewModel addImageView);
		void DeleteCarImage(ICollection<CarImage> carImages);
	}
}
