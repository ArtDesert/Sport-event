using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportEvent.AppServices.Contracts.Entities;

namespace SportEvent.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
	public void Configure(EntityTypeBuilder<Message> builder)
	{
		builder.ToTable(name: "messages", schema: "dcs_sport_event");

		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.IsRequired()
			.HasColumnName("id");

		builder.Property(x => x.Action)
			.HasMaxLength(50)
			.IsUnicode()
			.HasColumnName("action");

		builder.Property(x => x.Subject)
			.HasMaxLength(50)
			.IsUnicode()
			.HasColumnName("subject");

		builder.Property(x => x.Value)
			.HasMaxLength(50)
			.IsUnicode()
			.HasColumnName("value");

		builder.Property(x => x.Info)
			.HasMaxLength(50)
			.IsUnicode()
			.IsRequired()
			.HasColumnName("info");

		builder.Property(x => x.Time)
			.IsRequired()
			.HasColumnName("time");

		builder.Property(x => x.BroadcastId)
			.IsRequired()
			.HasColumnName("broadcast_id");

		builder.HasOne(message => message.Broadcast)
			.WithMany(broadcast => broadcast.Messages)
			.HasForeignKey(message => message.BroadcastId);
	}
}
