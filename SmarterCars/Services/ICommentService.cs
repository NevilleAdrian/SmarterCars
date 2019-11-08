using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.Services
{
	public interface ICommentService
	{
		Task<List<Comment>> GetAllComments();
		Task<Comment> GetCommentByID(int id);
		bool AddComment(Comment comment);
		Task<bool> UpdateCommentWithID(int id, Comment comment);
		Task<(bool, string)> DeleteCommentWithID(int id);
		Task<List<Comment>> GetAllCommentsByContentID(int id);
	}
}
