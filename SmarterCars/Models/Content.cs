using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class Content
	{
		[Key]
		public int ContentId { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string ShortDescription { get; set; }
		[Required]
		[DataType(DataType.Html)]
		public string LongDescription { get; set; }
		public DateTime DatePosted { get; set; }
		[DataType(DataType.ImageUrl)]
		public string ImagePath { get; set; }
		public bool ShowOnHome { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
		public int CategoryId { get; set; }
		public List<Comment> Comments { get; set; }
	}
}