using Elixir.SecurityQuestions.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Elixir.SecurityQuestions.Data
{
	public class SecurityQuestionContext : DbContext
	{
		private readonly IConfigurationRoot _config;

		public SecurityQuestionContext(IConfigurationRoot config)
		{
			_config = config;
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Question> Questions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder bldr)
		{
			base.OnConfiguring(bldr);

			var dataSource = _config.GetValue<string>("DataSource");
			if (dataSource == "InMemory")
			{
				bldr.UseInMemoryDatabase("SecurityQuestions");
			}
			else
			{
				var connectionString = _config.GetConnectionString("ESQConnectionString");
				bldr.UseSqlServer(connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);

			modelBuilder.Entity<User>()
				.HasAlternateKey(u => u.Name);

			modelBuilder.Entity<Question>()
				.HasAlternateKey(q => q.Query);
		}
	}
}
