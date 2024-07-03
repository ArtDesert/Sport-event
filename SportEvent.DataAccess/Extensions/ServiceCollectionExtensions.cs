using Microsoft.Extensions.DependencyInjection;
using SportEvent.AppServices.Contracts.BaseContracts;
using SportEvent.DataAccess.Repositories;

namespace SportEvent.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		return services.AddScoped<IBroadcastRepository, BroadcastRepository>()
					   .AddScoped<IMessageRepository, MessageRepository>();
	}
}
