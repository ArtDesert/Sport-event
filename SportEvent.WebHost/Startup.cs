using SportEvent.WebHost.Extensions;
using SportEvent.WebHost.Hubs;

namespace SportEvent.WebHost;

public class Startup
{
	private readonly IConfiguration _configuration;

	public Startup(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();

		services.AddEndpointsApiExplorer()
				.AddSwaggerGen()
				.AddHealthChecks();

		services.AddCustomServices()
				.AddDbContext(_configuration)
				.AddSignalR();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseSwagger()
			   .UseSwaggerUI();
		}
		app.UseRouting()
		   .UseHealthChecks("/health")
		   .UseEndpoints(endpoints =>
		   {
			   endpoints.MapControllers();
			   endpoints.MapHub<BroadcastHub>("/hub");
		   });
	}
}
