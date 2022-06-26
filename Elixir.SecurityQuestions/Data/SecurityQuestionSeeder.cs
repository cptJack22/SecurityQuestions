using Elixir.SecurityQuestions.Data.Entities;

namespace Elixir.SecurityQuestions.Data
{
	public class SecurityQuestionSeeder
	{
		private readonly SecurityQuestionContext _ctx;

		public SecurityQuestionSeeder(SecurityQuestionContext ctx)
		{
			_ctx = ctx;
		}

		public void Seed()
		{
			_ctx.Database.EnsureCreated();

			if (!_ctx.Questions.Any())
			{
				//	seed questions here
				_ctx.Add(new Question() { Query = "In what city were you born?" });
				_ctx.Add(new Question() { Query = "What is the name of your favorite pet?" });
				_ctx.Add(new Question() { Query = "What is your mother's maiden name?" });
				_ctx.Add(new Question() { Query = "What high school did you attend?" });
				_ctx.Add(new Question() { Query = "What was the mascot of your high school?" });
				_ctx.Add(new Question() { Query = "What was the make of your first car?" });
				_ctx.Add(new Question() { Query = "What was your favorite toy as a child?" });
				_ctx.Add(new Question() { Query = "Where did you meet your spouse?" });
				//_ctx.Add(new Question() { Query = "Where did you meet MY spouse? (all responses will be met with skepticism)" });
				_ctx.Add(new Question() { Query = "What is your favorite meal?" });
				_ctx.Add(new Question() { Query = "Who is your favorite actor / actress?" });
				_ctx.Add(new Question() { Query = "What is your favorite album?" });
				_ctx.Add(new Question() { Query = "What is your favorite movie?" });
				_ctx.Add(new Question() { Query = "What is your favorite Muppet?" });
				_ctx.Add(new Question() { Query = "What is your favorite holiday?" });
				_ctx.Add(new Question() { Query = "What is your quest?" });
				_ctx.Add(new Question() { Query = "What is your favorite color?" });
				_ctx.Add(new Question() { Query = "What is the airspeed velocity of the unladen swallow?" });
			}

			if (!_ctx.Users.Any())
			{
				//	seed users here
				_ctx.Add(new User() { Name = "Jack", });
			}

			_ctx.SaveChanges();
		}
	}
}
