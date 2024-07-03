using SportEvent.Api.Contracts.Messages.Requests;

namespace SportEvent.AppServices.Contracts.Messages.Handlers;
public interface ICreateMessageHandler
{
	Task<long> HandleAsync(CreateMessageRequest messageRequest, CancellationToken token = default);
}
