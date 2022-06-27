using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Elixir.SecurityQuestions.Data
{
	public class SecurityQuestionRepository : ISecurityQuestionRepository
	{
		private readonly SecurityQuestionContext _ctx;
		private readonly ILogger<SecurityQuestionRepository> _logger;

		public SecurityQuestionRepository(
			SecurityQuestionContext ctx,
			ILogger<SecurityQuestionRepository> logger)
		{
			_ctx = ctx;
			_logger = logger;
		}


		#region Questions
		public IEnumerable<Question> GetAllQuestions()
		{
			return _ctx.Questions
				.OrderBy(q => q.Id);
		}

		public Question GetQuestionById(int id)
		{
			var question = _ctx.Questions
				.Where(q => q.Id == id)
				.First();

			return question;
		}

		public Question GetQuestionByQuery(string query)
		{
			var question = _ctx.Questions
				.Where(q => q.Query == query)
				.First();

			return question;
		}
		#endregion //	questions

		#region Responses
		public void AddUserQnA(User user, Question question, string answer)
		{
			user.AddAnswer(question, answer);
		}
		#endregion //	responses

		#region Users
		public User GetUserById(Guid id)
		{
			var user = _ctx.Users
				.Include(u => u.Responses)
				.ThenInclude(r => r.Question)
				.SingleOrDefault(u => u.Id == id);

			if (user == null)
			{
				user = new User()
				{
					Id = id,
					Name = String.Empty,
				};
			}
			return user;
		}

		public User GetUserByName(string userName)
		{
			userName = userName.Trim();

			var user = _ctx.Users
				.Include(u => u.Responses)
				.ThenInclude(r => r.Question)
				.Where(u => string.Compare(u.Name, userName) == 0)
				.SingleOrDefault();

			if (user == null)
			{
				user = new User()
				{
					Id = Guid.Empty,
					Name = userName,
				};
			}

			return user;
		}
		#endregion //	users

		public void AddEntity(object entity)
		{
			_ctx.Add(entity);
		}

		public bool SaveAll()
		{
			return _ctx.SaveChanges() > 0;
		}
	}
}
