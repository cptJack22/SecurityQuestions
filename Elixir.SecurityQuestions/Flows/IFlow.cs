using Elixir.SecurityQuestions.Data.Entities;

namespace Elixir.SecurityQuestions.Flows
{
	public interface IFlow
	{
		public User User { get; set; }
		public FlowControl PerformFlow();
	}
}
