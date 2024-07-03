using Microsoft.Extensions.DependencyInjection;
using SportEvent.AppServices.Broadcasts.Handlers;
using SportEvent.AppServices.Contracts.Broadcasts.Handlers;
using SportEvent.AppServices.Contracts.Messages.Handlers;
using SportEvent.AppServices.Messages.Handlers;

namespace SportEvent.AppServices.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddHandlers(this IServiceCollection services)
	{
		return services.AddScoped<ICreateBroadcastHandler, CreateBroadcastHandler>()
					   .AddScoped<ICreateMessageHandler, CreateMessageHandler>()
					   .AddScoped<IChangeStatusHandler, ChangeStatusHandler>()
					   .AddScoped<IGetBroadcastByIdHandler, GetBroadcastByIdHandler>()
					   .AddScoped<IGetBroadcastsByDateHandler, GetBroadcastsByDateHandler>();
	}
}
