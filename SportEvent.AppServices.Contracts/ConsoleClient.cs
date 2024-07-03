using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace SportEvent.AppServices.Contracts;

public abstract class ConsoleClient
{
	protected static HubConnection HubConnection;
	private readonly IConfiguration _configuration;
    public ConsoleClient()
    {
		string path = Directory.GetCurrentDirectory();
		string parentDirectory = Directory.GetParent(path).Parent.Parent.Parent.FullName;
		string targetDirectory = Path.Combine(parentDirectory, "SportEvent.WebHost");
		_configuration = new ConfigurationBuilder()
			.SetBasePath(targetDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

	}
    protected virtual async void Connect()
	{
		string broadcastHubUrl = _configuration
			.GetSection("BroadcastHubOptions")
			.GetSection("Endpoint_HttpUrl")
			.Value;

		HubConnection = new HubConnectionBuilder()
			.WithUrl(broadcastHubUrl)
			.Build();
		await HubConnection.StartAsync();
	}

	protected abstract void SubscribeHandlersToEvents();

	protected abstract void ExecuteActions();

	public void Run()
	{
		Connect();
		SubscribeHandlersToEvents();
		ExecuteActions();
	}
}
