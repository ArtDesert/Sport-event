using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.DataAccess.Configurations;

public class BroadcastConfiguration : IEntityTypeConfiguration<Broadcast>
{
	public void Configure(EntityTypeBuilder<Broadcast> builder)
	{
		builder.ToTable(name: "broadcasts", schema: "dcs_sport_event");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.IsRequired()
			.HasColumnName("id");

		builder.Property(x => x.HomeTeam)
			.HasMaxLength(50)
			.IsUnicode()
			.IsRequired()
			.HasColumnName("home_team");

		builder.Property(x => x.GuestTeam)
			.HasMaxLength(50)
			.IsUnicode()
			.IsRequired()
			.HasColumnName("guest_team");

		builder.Property(x => x.StartDate)
			.IsRequired()
			.HasColumnName("start_date");

		builder.Property(x => x.StartTime)
			.IsRequired()
			.HasColumnName("start_time");

		builder.Property(x => x.Status)
			.IsRequired()
			.HasColumnName("status");

		builder.HasMany(broadcast => broadcast.Messages)
			.WithOne(message => message.Broadcast)
			.HasForeignKey(message => message.BroadcastId);
	}
}
