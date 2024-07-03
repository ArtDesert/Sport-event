using SportEvent.Api.Contracts.Messages.Requests;
using SportEvent.Api.Contracts.Messages.Responses;

namespace SportEvent.Api.Contracts.Abstractions;

/// <summary>
/// Интерфейс контроллера для работы с сущностью сообщения
/// </summary>
public interface IMessageController
{
	/// <summary>
	/// Создаёт новое сообщение
	/// </summary>
	/// <param name="message"></param>
	/// <param name="token"></param>
	/// <returns>Идентификатор созданного сообщения</returns>
	Task<long> CreateMessageAsync(CreateMessageRequest message, CancellationToken token);

	/// <summary>
	/// Возвращает все сообщения по идентификатору трансляции
	/// </summary>
	/// <param name="broadcastId"></param>
	/// <param name="token"></param>
	/// <returns></returns>
	Task<ICollection<MessageResponse>> GetMessagesByBroadcastId(long broadcastId, CancellationToken token);
}
