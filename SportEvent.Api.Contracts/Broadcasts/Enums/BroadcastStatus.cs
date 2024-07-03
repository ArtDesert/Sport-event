namespace SportEvent.Api.Contracts.Broadcasts.Enums;

/// <summary>
/// Статус трансляции
/// </summary>
public enum BroadcastStatus
{
	/// <summary>
	/// Неизвестно
	/// </summary>
	None = 0,

	/// <summary>
	/// Трансляция не началась
	/// </summary>
	NotStarted = 1,

	/// <summary>
	/// Трансляция идёт
	/// </summary>
	InProgress = 2,

	/// <summary>
	/// Трансляция завершена
	/// </summary>
	Completed = 3,
}
