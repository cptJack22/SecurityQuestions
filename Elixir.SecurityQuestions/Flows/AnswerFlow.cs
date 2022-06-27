using Elixir.SecurityQuestions.Data;
using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.Extensions.Logging;

using Sharprompt;


namespace Elixir.SecurityQuestions.Flows
{
	public class AnswerFlow : IAnswerFlow
	{
		private readonly ILogger<StoreFlow> _logger;
		private readonly ISecurityQuestionRepository _repo;

		public User User { get; set; }

		public AnswerFlow(
			ILogger<StoreFlow> logger,
			ISecurityQuestionRepository repo)
		{
			_logger = logger;
			_repo = repo;
		}

		public FlowControl PerformFlow()
		{
			var flow = FlowControl.Answer;

			try
			{
				var responses = User.Responses;

				foreach (var resp in responses)
				{
					var answer = Prompt.Input<string>(resp.Question.Query);

					if (!string.IsNullOrEmpty(answer) && answer.ToLower() == resp.Response.ToLower())
					{
						Console.WriteLine($"\nCongratulations, {User.Name}! You correctly answered your security question!");
						Console.WriteLine("(Press any key)");
						Console.ReadKey();

						_logger.LogDebug($"{User.Name} answered a security question correctly.");

						return FlowControl.Initial;
					}
				}

				_logger.LogDebug($"{User.Name} has not answered any security questions correctly.");

				Console.WriteLine($"\nSorry, {User.Name}. You have not answered any security question correctly.");
				Console.WriteLine("(Press any key)");
				Console.ReadKey();

				flow = FlowControl.Initial;
			}
			catch (PromptCanceledException ex)
			{
				flow = FlowControl.Exit;

				_logger.LogError($"{ex.Message}");
				Console.WriteLine($"{ex.Message}");
			}

			return flow;
		}
	}
}
