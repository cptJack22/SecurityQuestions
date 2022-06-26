using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elixir.SecurityQuestions.Data
{
	public class SecurityQuestionInMemoryRepository : ISecurityQuestionRepository
	{
		private readonly SecurityQuestionContext _ctx;
		private readonly ILogger<SecurityQuestionInMemoryRepository> _logger;

		public SecurityQuestionInMemoryRepository(
			SecurityQuestionContext ctx,
			ILogger<SecurityQuestionInMemoryRepository> logger)
		{
			_ctx = ctx;
			_logger = logger;
		}


		#region Questions
		public IEnumerable<Question> GetAllQuestions()
		{
			return _ctx.Questions;
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

		public IEnumerable<UserResponse> GetUserQuestions(User user)
		{
			return user.Responses;
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
				.Where(u => u.Name.CompareTo(userName) == 0)
				.SingleOrDefault();

			if(user == null)
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
