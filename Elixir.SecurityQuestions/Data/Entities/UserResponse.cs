using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elixir.SecurityQuestions.Data.Entities
{
	public class UserResponse
	{
		#region Properties
		[Key]
		public int Id { get; set; }

		[ForeignKey("User")]
		public Guid UserId { get; set; }

		[Required]
		public Question Question { get; set; }

		[Required]
		[StringLength(500)]
		public string Response { get; set; }
		#endregion   //	properties
	}
}
