using Elixir.SecurityQuestions.Data;
using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.Extensions.Logging;

using Sharprompt;

namespace Elixir.SecurityQuestions.Flows
{
	public class InitialFlow : IInitialFlow
	{
		private readonly ILogger<InitialFlow> _logger;
		private readonly ISecurityQuestionRepository _repo;

		public User User { get; set; }

		public InitialFlow(
			ILogger<InitialFlow> logger,
			ISecurityQuestionRepository repo)
		{
			_logger = logger;
			_repo = repo;

		}

		public FlowControl PerformFlow()
		{
			Console.Clear();

			var flow = FlowControl.Initial;

			try
			{
				var name = Prompt.Input<string>("Hi, what is your name?");


				//	empty response
				if (string.IsNullOrEmpty(name))
				{
					return FlowControl.Initial;
				}

				//	"I'm done"
				if (name.ToLower() == "exit")
				{
					return FlowControl.Exit;
				}

				User = _repo.GetUserByName(name);

				Console.WriteLine($"\n\tHello, {User.Name}!\n");

				//	new user!
				if (User.Id == Guid.Empty)
				{
					_repo.AddEntity(User);
					if (_repo.SaveAll())
					{
						User = _repo.GetUserByName(User.Name);
					}
					else
					{
						throw new Exception($"Unable to save user {User.Name}");
					}
				}

				if (User.Responses == null || User.Responses.Count == 0)
				{
					//	go to store flow
					flow = FlowControl.Store;
				}
				else
				{
					var yorn = Prompt.Confirm("Do you want to answer a security question?", defaultValue: true);
					if (yorn)
					{
						//	go to answer flow
						flow = FlowControl.Answer;
					}
					else
					{
						//	go to store flow
						flow = FlowControl.Store;
					}
				}
			}
			catch (PromptCanceledException ex)
			{
				flow = FlowControl.Exit;
				Console.WriteLine($"{ex.Message}");
			}
			catch (Exception ex)
			{
				flow = FlowControl.Exit;
				Console.WriteLine($"{ex.Message}");
			}

			return flow;
		}
	}
}
