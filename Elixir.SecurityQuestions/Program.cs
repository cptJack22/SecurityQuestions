
using Sharprompt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Elixir.SecurityQuestions.Data;
using System.ComponentModel.DataAnnotations;
using Elixir.SecurityQuestions.Data.Entities;
using Elixir.SecurityQuestions.Flows;

namespace Elixir.SecurityQuestions
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//	setup DI
			IServiceCollection services = new ServiceCollection();

			//	startup class added
			Startup startup = new Startup();
			startup.ConfigureServices(services);
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			//	configure console logging
			serviceProvider
				.GetService<ILoggerFactory>();
			//.AddConsole(LogLevel.Debug);

			var logger = serviceProvider.GetService<ILoggerFactory>()
				.CreateLogger<Program>();

			logger.LogDebug("Starting application");

			//	Setup db
			var seeder = serviceProvider.GetService<SecurityQuestionSeeder>();
			seeder.Seed();

			// Get Service and call method
			//var repo = serviceProvider.GetService<ISecurityQuestionRepository>();
			var initialFlow = serviceProvider.GetService<IInitialFlow>();
			var storeFlow = serviceProvider.GetService<IStoreFlow>();
			var answerFlow = serviceProvider.GetService<IAnswerFlow>();
			//var service = serviceProvider.GetService<IMyService>();
			//service.MyServiceMethod();


			//	run application
			FlowControl flow = FlowControl.Initial;
			User user = null;

			while (flow != FlowControl.Exit)
			{
				switch (flow)
				{
					case FlowControl.Initial:
						flow = initialFlow.PerformFlow();
						user = initialFlow.User;
						break;

					case FlowControl.Store:
						storeFlow.User = user;
						flow = storeFlow.PerformFlow();
						break;
					
					case FlowControl.Answer:
						answerFlow.User = user;
						flow = answerFlow.PerformFlow();
						break;

					case FlowControl.Exit:
						break;
				}
			}

			return;
		}
	}
}