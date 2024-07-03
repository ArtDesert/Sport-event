using Microsoft.EntityFrameworkCore;
using SportEvent.AppServices.Contracts.Entities;
using System.Reflection;

namespace SportEvent.DataAccess;

public class SportEventContext : DbContext
{
	public DbSet<Broadcast> Broadcasts { get; set; }
	public DbSet<Message> Messages { get; set; }

    public SportEventContext(DbContextOptions<SportEventContext> options) : base(options)
    {
        
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}
