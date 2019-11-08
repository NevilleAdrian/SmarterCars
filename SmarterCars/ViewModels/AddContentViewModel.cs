using Microsoft.AspNetCore.Http;
using SmarterCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmarterCars.ViewModels
{
	public class AddContentViewModel
	{
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public IFormFile File { get; set; }
		public bool ShowOnHome { get; set; }
		public int CategoryId { get; set; }
	}

	public class EditContentViewModel
	{
		public int ContentId { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public string OldImagePath { get; set; }
		public IFormFile NewFile { get; set; }
		public bool ShowOnHome { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}

	public class EmitContentViewModel
	{
		public int ContentId { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string LongDescription { get; set; }
		public string ImagePath { get; set; }
		public DateTime DatePosted { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public List<Comment> Comments { get; set; }
		public bool ShowOnHome { get; set; }
	}

	public class DeleteContentViewModel
	{
		public int Id { get; set; }
		public List<string> ImagesToDelete { get; set; }
	}

	public class ContentPageViewModel
	{
		public EmitContentViewModel Content { get; set; }
		public List<CarViewForHome> CarViews { get; set; }
	}
}
