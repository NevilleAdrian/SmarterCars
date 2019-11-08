using Microsoft.EntityFrameworkCore;
using SmarterCars.Data;
using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public class CommentService : ICommentService
	{
		private readonly ApplicationDbContext _ctx;
		public CommentService(ApplicationDbContext context)
		{
			_ctx = context;
		}

		public bool AddComment(Comment comment)
		{
			comment.DateAdded = DateTime.Now;
			comment.ShouldPublish = true;
			try
			{
				_ctx.Comments.Add(comment);
				_ctx.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<(bool, string)> DeleteCommentWithID(int id)
		{
			if (CommentExists(id))
			{
				var target = await _ctx.Comments.Where(c => c.CommentId == id).SingleOrDefaultAsync();
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
			return (false, "404");
		}

		public async Task<List<Comment>> GetAllComments()
		{
			return await _ctx.Comments.Include(c => c.ApplicationUser).ToListAsync() ?? new List<Comment>();
		}

		public async Task<Comment> GetCommentByID(int id)
		{
			if (CommentExists(id))
			{
				return await _ctx.Comments.Include(c => c.ApplicationUser).Where(c => c.CommentId == id).SingleOrDefaultAsync();
			}
			return null;
		}

		public async Task<List<Comment>> GetAllCommentsByContentID(int id)
		{

			return await _ctx.Comments.Include(c => c.ApplicationUser).Where(c => c.ContentId == id).ToListAsync() ?? new List<Comment>();
		}

		public async Task<bool> UpdateCommentWithID(int id, Comment comment)
		{
			if (CommentExists(id))
			{
				if (id != comment.CommentId)
				{
					return false;
				}

				var target =  await _ctx.Comments.Where(c => c.CommentId == id)
					.Select(c => new Comment {
						CommentId = c.CommentId,
						Message = comment.Message != null ? comment.Message : c.Message,
						ContentId = c.ContentId,
						UserId = c.UserId,
						DateAdded = c.DateAdded,
						ShouldPublish = comment.ShouldPublish
					}).SingleOrDefaultAsync();

				try
				{
					_ctx.Update(target);
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

		public bool CommentExists(int id) => _ctx.Comments.Any(c => c.CommentId == id);
	}
}
