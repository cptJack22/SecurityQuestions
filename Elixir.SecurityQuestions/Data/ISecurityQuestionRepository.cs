using Elixir.SecurityQuestions.Data.Entities;

namespace Elixir.SecurityQuestions.Data
{
	public interface ISecurityQuestionRepository
	{
		#region Questions
		IEnumerable<Question> GetAllQuestions();

		Question GetQuestionById(int id);
		
		Question GetQuestionByQuery(string query);
		#endregion //	questions

		#region Responses
		IEnumerable<UserResponse> GetUserQuestions(User user);
		#endregion    //	responses

		#region Users
		void AddUserQnA(User user, Question question, string answer);

		User GetUserById(Guid id);
		
		User GetUserByName(string userName);
		#endregion  //	users

		void AddEntity(object entity);

		bool SaveAll();
	}
}
