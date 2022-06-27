using Elixir.SecurityQuestions.Data;
using Elixir.SecurityQuestions.Flows;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Sharprompt;

namespace Elixir.SecurityQuestions
{
	public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            PromptConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Clear();

            services.AddDbContext<SecurityQuestionContext>(config =>
			{
				config.UseSqlServer();
			});
            
            //	db seeder
            services.AddTransient<SecurityQuestionSeeder>();

            //  logging
            services.AddLogging(loggerBuilder =>
            {
                loggerBuilder.ClearProviders();
                loggerBuilder.AddConsole();
            });

            //  configuration
            services.AddSingleton<IConfigurationRoot>(Configuration);

            //	repositories
            services.AddScoped<ISecurityQuestionRepository, SecurityQuestionRepository>();

            //  flows
            services.AddSingleton<IInitialFlow, InitialFlow>();
            services.AddSingleton<IStoreFlow, StoreFlow>();
            services.AddSingleton<IAnswerFlow, AnswerFlow>();
        }

        private void PromptConfiguration()
        {
            Prompt.ColorSchema.Answer = ConsoleColor.Green;
            Prompt.ColorSchema.Select = ConsoleColor.DarkCyan;
            Prompt.ThrowExceptionOnCancel = true;
        }
    }
}
