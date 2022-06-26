using Elixir.SecurityQuestions.Data;
using Elixir.SecurityQuestions.Flows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Sharprompt;

namespace Elixir.SecurityQuestions
{
	public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            PromptConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Clear();

            services.AddDbContext<SecurityQuestionContext>();
            
            //	db seeder
            services.AddTransient<SecurityQuestionSeeder>();

            //  loggin
            services.AddLogging();
            
            //  configuration
            services.AddSingleton<IConfigurationRoot>(Configuration);

            //	repositories
            services.AddScoped<ISecurityQuestionRepository, SecurityQuestionInMemoryRepository>();

            //  flows
            services.AddSingleton<IInitialFlow, InitialFlow>();
            services.AddSingleton<IStoreFlow, StoreFlow>();
            services.AddSingleton<IAnswerFlow, AnswerFlow>();
        }

        private void PromptConfiguration()
        {
            //Prompt.Symbols.Done = new Symbol("->", "V");
            Prompt.ColorSchema.Answer = ConsoleColor.Green;
            Prompt.ColorSchema.Select = ConsoleColor.DarkCyan;
            Prompt.ThrowExceptionOnCancel = true;
        }
    }
}
