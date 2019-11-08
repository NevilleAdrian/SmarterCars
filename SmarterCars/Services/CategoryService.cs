using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmarterCars.Data;
using SmarterCars.Models;

namespace SmarterCars.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ApplicationDbContext _ctx;

		public CategoryService(ApplicationDbContext context)
		{
			_ctx = context;
		}
		public bool AddCategory(Category category)
		{
			try
			{
				_ctx.Categories.Add(category);
				_ctx.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<(bool, string)> DeleteCategoryWithID(int id)
		{
			if (CategoryExists(id))
			{
				var target = await _ctx.Categories.FindAsync(id);
				if (target != null)
				{
					try
					{
						_ctx.Remove(target);
						await _ctx.SaveChangesAsync();
						return (true, "200");
					}

					catch
					{
						return (false, "500");
					}
				}
			}
			return (false, "404");
		}

		public async Task<List<Category>> GetAllCategories(bool includeContents)
		{
			if (includeContents)
			{
				return await _ctx.Categories.Include(c => c.Contents).ToListAsync();
			}
			return await _ctx.Categories.ToListAsync();
		}

		public async Task<Category> GetCategoryByID(int id, bool includeContents)
		{
			if (CategoryExists(id))
			{
				if (includeContents)
				{
					return await _ctx.Categories.Include(c => c.Contents).ThenInclude(co => co.Comments).SingleOrDefaultAsync(c => c.CategoryId == id);
				}
				return await _ctx.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);

			}
			return null;
		}

		public async Task<bool> UpdateCategoryWithID(int id, Category category)
		{
			if (CategoryExists(id))
			{
				if (id != category.CategoryId)
				{
					return false;
				}
				try
				{
					_ctx.Update(category);
					await _ctx.SaveChangesAsync();
					return true;
				}
				catch
				{
					return false; 
				}

			}
			return false;
		}

		public bool CategoryExists(int id) => _ctx.Categories.Any(c => c.CategoryId == id);
	}
}
