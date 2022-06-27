using System.ComponentModel.DataAnnotations;

namespace Elixir.SecurityQuestions.Data.Entities
{
	public class Question
	{
		#region Properties
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(500)]
		public string Query { get; set; }
		#endregion   //	properties
	}
}
