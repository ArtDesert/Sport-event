using DbUp;
using DbUp.Engine;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SportEvent.Migrations;

public class Program
{
	static void Main(string[] args)
	{
		string path = Directory.GetCurrentDirectory();
		string parentDirectory = Directory.GetParent(path).Parent.Parent.Parent.FullName;
		string targetDirectory = Path.Combine(parentDirectory, "SportEvent.WebHost");
		IConfiguration configuration = new ConfigurationBuilder()
			.SetBasePath(targetDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

        var connection = configuration.GetConnectionString("sport-event-db-service-connection");
        UpgradeEngine builder = DeployChanges.To
			.PostgresqlDatabase(connection)
			.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
			.LogToConsole()
			.Build();

		DatabaseUpgradeResult result = builder.PerformUpgrade();

		if (!result.Successful)
		{
			throw new Exception("Не удалось применить миграции к базе данных.", result.Error);
		}
		else
		{
			Console.WriteLine("SportEventService миграции завершены, приложение готово к запуску.");
		}
	}
}
