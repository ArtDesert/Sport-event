using Microsoft.AspNetCore.SignalR.Client;
using SportEvent.Api.Contracts.Broadcasts.Enums;
using SportEvent.Api.Contracts.Broadcasts.Requests;

namespace SportEvent.ConsoleClient.Commentator;

public class CommentatorClient : AppServices.Contracts.ConsoleClient
{
	private static long CurrentBroadcastId { get; set; } = -1;

	protected async override void ExecuteActions()
	{
        await Console.Out.WriteLineAsync("Добро пожаловать! Введите '--help' для просмотра возможных команд.");
        while (true)
		{
			var command = await Console.In.ReadLineAsync();
			if (command == "--help")
			{
                await Console.Out.WriteLineAsync("\'register\' - зарегистрировать трансляцию\r\n\'start\' - запустить трансляцию\r\n\'send\' - отправить сообщение всем зрителяем в одном из следующих форматов:\r\n1. 'действие;субъект;основная информация'\r\n2. 'действие;дополнительная информация о действии(например счёт);субъект;основная информация'\r\n\'end\' - завершить трансляцию");
            }
			else if (command == "register")
			{
				await Console.Out.WriteLineAsync("Введите название команды, которая играет дома:");
				string homeTeam = await Console.In.ReadLineAsync();
				await Console.Out.WriteLineAsync("Введите название команды, которая играет в гостях:");
				string guestTeam = await Console.In.ReadLineAsync(); 
				await Console.Out.WriteLineAsync("Введите дату начала трансляции в формате \'ГГГГ-ММ-ДД\':");
				DateOnly startDate;
				while (!DateOnly.TryParse(await Console.In.ReadLineAsync(), out startDate))
				{
                    await Console.Out.WriteLineAsync("Некорректный формат! Введите дату начала трансляции в формате 'ГГГГ-ММ-ДД':");
                }
                await Console.Out.WriteLineAsync("Введите время начала трансляции в формате 'ЧЧ:ММ:CC':");
                TimeOnly startTime;
				while (!TimeOnly.TryParse(await Console.In.ReadLineAsync(), out startTime))
				{
					await Console.Out.WriteLineAsync("Некорректный формат! Введите время начала трансляции в формате 'ЧЧ:ММ:CC':");
				}
				var broadcastRequest = new CreateBroadcastRequest()
				{
					HomeTeam = homeTeam,
					GuestTeam = guestTeam,
					StartDate = startDate,
					StartTime = startTime
				};
				await HubConnection.SendAsync("RegisterBroadcast", broadcastRequest);
                await Console.Out.WriteLineAsync("Трансляция успешно зарегистирована!");
            }
			else if (command == "start")
			{
				if (CurrentBroadcastId != -1)
				{
                    await Console.Out.WriteLineAsync($"В данный момент уже запущена трансляция под номером {CurrentBroadcastId}.");
                }
				else
				{
					await Console.Out.WriteLineAsync("Введите номер трансляции, которую хотите начать:");
					CurrentBroadcastId = long.Parse(await Console.In.ReadLineAsync());
					await HubConnection.SendAsync("CheckBroadcastStatusForCommentator", CurrentBroadcastId);
				}
            }
			else if (command == "send")
			{
				if (CurrentBroadcastId == -1)
				{
					await Console.Out.WriteLineAsync($"Для отправки сообщений зрителям нужно запустить трансляцию.");
				}
				else
				{
					await Console.Out.WriteLineAsync("Введите сообщение:");
					string message = await Console.In.ReadLineAsync();
					await HubConnection.SendAsync("AddMessageIntoDb", CurrentBroadcastId, message);
                    await Console.Out.WriteLineAsync("Сообщение отправлено.");
                    await HubConnection.SendAsync("SendMessageToGroup", CurrentBroadcastId.ToString(), message);
                }
			}
			else if (command == "end")
			{
				if (CurrentBroadcastId == -1)
				{
					await Console.Out.WriteLineAsync($"Eщё не запущено ни одной трансляции.");
				}
				else
				{
					await HubConnection.SendAsync("ChangeStatus", CurrentBroadcastId, BroadcastStatus.Completed);
					await Console.Out.WriteLineAsync("Трансляция завершена!");
					CurrentBroadcastId = -1;
				}
			}
			else 
			{
				Console.WriteLine("Неизвестная команда! Введите \'--help\' для просмотра возможных команд.");
			}
		}
	}

	protected override void SubscribeHandlersToEvents()
	{
		HubConnection.On<bool>("GetResultForCommentator", (isCorrectBroadcastId) =>
		{
			if (!isCorrectBroadcastId)
			{
				Console.WriteLine($"Трансляция с номером {CurrentBroadcastId} ещё не началась либо уже закончилась!");
				CurrentBroadcastId = -1;
			}
			else
			{
				HubConnection.SendAsync("ChangeStatus", CurrentBroadcastId, BroadcastStatus.InProgress);
				Console.Out.WriteLineAsync("Трансляция запущена!");
			}
		});
	}
}
