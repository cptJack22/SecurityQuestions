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
		public string Name { get; set; }

		[MaxLength(3)]
		public List<UserResponse> Responses { get; set; }
		#endregion   //	properties

		#region Methods
		public void AddAnswer(Question question, string answer)
		{
			if (Responses != null && Responses.Count < 3)
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
