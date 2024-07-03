using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.Api.Contracts.Broadcasts.Requests;
using SportEvent.Api.Contracts.Broadcasts.Responses;

namespace SportEvent.Api.Contracts.Abstractions;

/// <summary>
/// Интерфейс контроллера для работы с сущностью трансляции
/// </summary>
public interface IBroadcastController
{

	/// <summary>
	/// Создаёт новую трансляцию
	/// </summary>
	/// <param name="broadcast"></param>
	/// <param name="token"></param>
	/// <returns>Идентификатор созданной трансляции</returns>
	Task<long> CreateBroadcastAsync(CreateBroadcastRequest broadcast, CancellationToken token);

	/// <summary>
	/// Возвращает все трансляции на указанную дату
	/// </summary>
	/// <param name="date"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<IEnumerable<BroadcastResponse>> GetBroadcastsByDateAsync(DateOnly date, CancellationToken token);

	/// <summary>
	/// Меняет статус трансляции на заданный
	/// </summary>
	/// <param name="broadcastId"></param>
	/// <param name="newStatus"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task ChangeBroadcastStatus(long broadcastId, BroadcastStatus newStatus, CancellationToken token);

	/// <summary>
	/// Возвращает трансляцию по её идентификатору
	/// </summary>
	/// <param name="broadcastId"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<BroadcastResponse> GetBroadcastByIdAsync(long broadcastId, CancellationToken token);
}
