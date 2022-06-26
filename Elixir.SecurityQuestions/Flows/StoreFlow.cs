using Elixir.SecurityQuestions.Data;
using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.Extensions.Logging;

using Sharprompt;

namespace Elixir.SecurityQuestions.Flows
{
	public class StoreFlow : IStoreFlow
	{
		private readonly ILogger<StoreFlow> _logger;
		private readonly ISecurityQuestionRepository _repo;

		public User User { get; set; }

		public StoreFlow(
			ILogger<StoreFlow> logger,
			ISecurityQuestionRepository repo)
		{
			_logger = logger;
			_repo = repo;
		}

		public FlowControl PerformFlow()
		{
			var flow = FlowControl.Store;

			try
			{
				Console.WriteLine();
				var yorn = Prompt.Confirm("Would you like to store answers to security questions?", defaultValue: true);

				if (!yorn)
				{
					return FlowControl.Initial;
				}

				User.Responses = new List<UserResponse>();

				var qCount = User.Responses.Count();
				var arrQs = _repo.GetAllQuestions().ToList();

				while (qCount < 3)
				{
					Console.WriteLine();
					var q = Prompt.Select($"Select a question to answer ({qCount}/3 answered)", arrQs.Select(q => q.Query).ToArray());
					var question = _repo.GetQuestionByQuery(q);

					var answer = Prompt.Input<string>(question.Query);

					User.AddAnswer(question, answer);

					arrQs.Remove(question);

					qCount = User.Responses.Count();
				}

				_repo.SaveAll();

				flow = FlowControl.Initial;
			}
			catch (PromptCanceledException ex)
			{
				flow = FlowControl.Exit;
				Console.WriteLine($"{ex.Message}");
			}

			return flow;
		}
	}
}
