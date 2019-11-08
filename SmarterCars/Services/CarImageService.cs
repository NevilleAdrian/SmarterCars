using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
	public class CarImageService : ICarImageService
	{
		private readonly ApplicationDbContext _ctx;
		private readonly IImageService _imgService;
		private readonly IHostingEnvironment _env;
		private readonly ILogger<CarImageService> _logger;
		public CarImageService(ApplicationDbContext context, IImageService imageService, ILogger<CarImageService> logger, IHostingEnvironment env)
		{
			_ctx = context;
			_imgService = imageService;
			_env = env;
			_logger = logger;
		}

		public bool AddCarImage(AddImageViewModel addImageView)
		{
			if (addImageView.File != null && !string.IsNullOrEmpty(addImageView.Description) && !string.IsNullOrEmpty(addImageView.View))
			{
				string path = _imgService.CreateImage(addImageView.File, AppConstant.ImageFolder, _env);
				if (!string.IsNullOrEmpty(path))
				{
					CarImage carImageToAdd = new CarImage {
						CarId = addImageView.CarId,
						CarImagePath = path,
						Description = addImageView.Description,
						View = addImageView.View
					};

					try
					{
						_ctx.CarImages.Add(carImageToAdd);
						_ctx.SaveChanges();
						return true;
					}
					catch (DbUpdateException ex) {
						_logger.LogInformation($"{ex.Message}");
					}
				}
			}
			return false;
		}

		public void AddCarImages(int carId, ICollection<CarImageDto> carImageDtos)
		{
			try
			{
				IEnumerable<CarImage> cars = carImageDtos.Select(cI => new CarImage
				{
					Description = cI.Description,
					View = cI.View,
					CarImagePath = _imgService.CreateImage(cI.CarImage, AppConstant.ImageFolder, _env),
					CarId = carId
				});

				_ctx.CarImages.AddRange(cars);
				_ctx.SaveChanges();
			}
			catch (Exception ex)
			{
				_logger.LogInformation($"Car Image could not be saved because of {ex.Message}");
			}

		}

		public string EditCarImage(EditImageViewModel editImageView)
		{
			if (CarImageExists(editImageView.CarImageId))
			{
				string imagePath = _imgService.EditImage(editImageView.File, editImageView.ImageToRemove, AppConstant.ImageFolder, _env);
				
				CarImage carImageToEdit = _ctx.CarImages.Where(cI => cI.CarImageId == editImageView.CarImageId).Select(cI => new CarImage {
					Description = editImageView.Description ?? cI.Description,
					View = editImageView.View ?? cI.View,
					CarId = cI.CarId,
					CarImageId = cI.CarImageId,
					CarImagePath = imagePath ?? cI.CarImagePath
				}).SingleOrDefault();
				try
				{
					_ctx.CarImages.Update(carImageToEdit);
					_ctx.SaveChanges();
				}
				catch (DbUpdateException ex)
				{
					_logger.LogInformation($"Could not update file because of {ex.Message}");
				}
					
				return imagePath;
				
			}
			return null;
		}

		public bool DeleteCarImage(EditImageViewModel editImageView)
		{
			bool removed = false;
			if (CarImageExists(editImageView.CarImageId))
			{
				removed = _imgService.DeleteImage(editImageView.ImageToRemove, _env);

				CarImage carImageToEdit = _ctx.CarImages.Where(cI => cI.CarImageId == editImageView.CarImageId).SingleOrDefault();
				try
				{
					_ctx.CarImages.Remove(carImageToEdit);
					_ctx.SaveChanges();
				}
				catch (DbUpdateException ex)
				{
					_logger.LogInformation($"Could not update file because of {ex.Message}");
				}

			}
			return removed;
		}

		public void DeleteCarImage(ICollection<CarImage> carImages)
		{
			if (carImages != null)
			{
				try
				{
					_ctx.CarImages.RemoveRange(carImages);
					_ctx.SaveChanges();
				}
				catch { }
			}
		}

		public bool CarImageExists(int cId) => _ctx.CarImages.Any(id => id.CarImageId == cId);
	}

	
}
