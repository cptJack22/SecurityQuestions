using System.ComponentModel.DataAnnotations;

namespace Elixir.SecurityQuestions.Data.Entities
{
	public class User
	{
		#region Properties
		[Key]
		public Guid Id { get; set; }

		[Display(Name = "Hi, what is your name?")]
		[Required]
		[MaxLength(255)]
		public string Name { get; set; }

		[MaxLength(Constants.REQUIRED_QUESTION_COUNT)]
		public List<UserResponse> Responses { get; set; }
		#endregion   //	properties

		#region Methods
		public void AddAnswer(Question question, string answer)
		{
			if (Responses != null && Responses.Count < Constants.REQUIRED_QUESTION_COUNT)
			{
				var response = new UserResponse()
				{
					Question = question,
					Response = answer,
					UserId = Id
				};

				Responses.Add(response);
			}
		}
		#endregion	//	methods
	}
}
