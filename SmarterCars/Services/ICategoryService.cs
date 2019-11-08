using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public interface ICategoryService
	{
		Task<List<Category>> GetAllCategories(bool includeContents);
		Task<Category> GetCategoryByID(int id, bool includeContents);
		bool AddCategory(Category category);
		Task<bool> UpdateCategoryWithID(int id, Category category);
		Task<(bool, string)> DeleteCategoryWithID(int id);
	}
}
