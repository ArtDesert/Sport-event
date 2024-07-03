
using SportEvent.Api.Contracts.Broadcasts.Enums;

namespace SportEvent.AppServices.Contracts.Entities;

/// <summary>
/// Трансляция
/// </summary>
public class Broadcast
{
    /// <summary>
    /// Идентификатор трансляции
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Команда, которая играет дома
    /// </summary>
    public string HomeTeam { get; set; }

    /// <summary>
    /// Команда, которая играет в гостях
    /// </summary>
    public string GuestTeam { get; set; }

    /// <summary>
    /// Дата начала трансляции
    /// </summary>
    public DateOnly StartDate { get; set; }

	/// <summary>
	/// Время начала трансляции
	/// </summary>
	public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Статус трансляции
    /// </summary>
    public BroadcastStatus Status { get; set; }

    /// <summary>
    /// Навигационное свойство для всех сообщений данной трансляции
    /// </summary>
	public virtual List<Message> Messages { get; set; }
}
