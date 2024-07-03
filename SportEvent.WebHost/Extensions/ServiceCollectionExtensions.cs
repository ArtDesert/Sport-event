using Microsoft.EntityFrameworkCore;
using SportEvent.Api.Contracts.Abstractions;
using SportEvent.AppServices.Extensions;
using SportEvent.DataAccess;
using SportEvent.DataAccess.Extensions;
using SportEvent.WebHost.Controllers;

namespace SportEvent.WebHost.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		return services.AddDbContext<SportEventContext>(optionsBuilder =>
		{
			optionsBuilder.UseNpgsql(configuration.GetConnectionString("sport-event-db-service-connection"))
						  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});
	}

	public static IServiceCollection AddCustomControllers(this IServiceCollection services)
	{
		return services
			.AddScoped<IBroadcastController, BroadcastController>()
			.AddScoped<IMessageController, MessageController>();
	}

	public static IServiceCollection AddCustomServices(this IServiceCollection services)
	{
		return services
			.AddCustomControllers()
			.AddHandlers()
			.AddRepositories();
	}
}
