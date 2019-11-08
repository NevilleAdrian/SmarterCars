using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmarterCars.Models
{
	public class Comment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CommentId { get; set; }
		[Required]
		[MaxLength(200)]
		public string Message { get; set; }
		public DateTime DateAdded { get; set; }
		public bool ShouldPublish { get; set; }
		[ForeignKey("UserId")]
		public ApplicationUser ApplicationUser { get; set; }
		public string UserId { get; set; }
		[ForeignKey("ContentId")]
		public Content Content { get; set; }
		public int ContentId { get; set; }
	}
}
