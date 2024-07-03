using Microsoft.AspNetCore.SignalR.Client;
using SportEvent.Api.Contracts.Broadcasts.Responses;
using SportEvent.Api.Contracts.Broadcasts.Enums;

namespace SportEvent.ConsoleClient.Fan;

public class FanClient : AppServices.Contracts.ConsoleClient
{
	private static long CurrentBroadcastId { get; set; } = -1;
	private static TimeOnly CurrentBroadcastStartTime { get; set; } = default;

	protected async override void ExecuteActions()
	{
		await Console.Out.WriteLineAsync("Добро пожаловать! Введите '--help' для просмотра возможных команд.");
		while (true)
		{
			var command = Console.ReadLine();
			if (command == "--help")
			{
				await Console.Out.WriteLineAsync("\'broadcasts\' - вывести все доступные трансляцию\r\n\'connect\' - подключиться к трансляции\r\n\'disconnect\' - отключиться от трансляции \r\n");
			}
			else if (command == "broadcasts")
			{
                await Console.Out.WriteLineAsync("Введите дату в формате ГГГГ-ММ-ДД:");
				DateOnly date;
				while (!DateOnly.TryParse(await Console.In.ReadLineAsync(), out date))
				{
					await Console.Out.WriteLineAsync("Некорректный формат! Введите дату в формате 'ГГГГ-ММ-ДД':");
				}
				await HubConnection.SendAsync("GetBroadcastsByDate", date);
			}
			else if (command == "connect")
			{
				if (CurrentBroadcastId != -1)
				{
                    await Console.Out.WriteLineAsync($"Вы уже подключены к трансляции с номером {CurrentBroadcastId}.");
				}
				else
				{
					await Console.Out.WriteLineAsync("Введите номер трансляции, к которой хотите подключиться:");
					CurrentBroadcastId = long.Parse(await Console.In.ReadLineAsync());
					await HubConnection.SendAsync("CheckBroadcastStatusForFan", CurrentBroadcastId);
				}
			}
			else if (command == "disconnect")
			{
				if (CurrentBroadcastId == -1)
				{
					await Console.Out.WriteLineAsync($"Вы ещё не подключены ни к одной трансляции.");
				}
				else 
				{
					await HubConnection.SendAsync("ExitGroup", CurrentBroadcastId.ToString());
					CurrentBroadcastId = -1;
					await Console.Out.WriteLineAsync("Вы отключились от трансляции.");
				}
			}
			else 
			{
                await Console.Out.WriteLineAsync("Неизвестная команда. Введите \'--help\' для просмотра возможных команд.");
            }
		}
	}

	protected override void SubscribeHandlersToEvents()
	{
		HubConnection.On<string>("Send", (message) =>
		{
			int minutes = int.Parse(DateTime.Now.ToShortTimeString().Split(":")[1]) - int.Parse(CurrentBroadcastStartTime.ToShortTimeString().Split(":")[1]);
			Console.Out.WriteLineAsync($"{minutes} минута; {message}");
		});
		HubConnection.On<IEnumerable<BroadcastResponse>>("SendBroadcasts", (broadcasts) =>
		{
			foreach (var broadcast in broadcasts)
			{
				switch (broadcast.Status)
				{
					case BroadcastStatus.NotStarted:
						Console.ForegroundColor = ConsoleColor.Yellow;
						break;
					case BroadcastStatus.InProgress:
						Console.ForegroundColor = ConsoleColor.Green;
						break;
					case BroadcastStatus.Completed:
						Console.ForegroundColor = ConsoleColor.Red;
						break;
					default:
						Console.ForegroundColor = ConsoleColor.DarkBlue;
						break;
				}
				Console.WriteLine(broadcast);
            }
			Console.ForegroundColor = ConsoleColor.White;
		});

		HubConnection.On<bool, TimeOnly>("GetResultForFan", (isCorrectBroadcastId, broadcastStartTime) =>
		{
			if (!isCorrectBroadcastId)
			{
				Console.WriteLine($"Трансляция с номером {CurrentBroadcastId} ещё не началась либо уже закончилась!");
				CurrentBroadcastId = -1;
			}
			else
			{
				CurrentBroadcastStartTime = broadcastStartTime;
				HubConnection.SendAsync("JoinGroup", CurrentBroadcastId.ToString());
				Console.WriteLine("Вы подключились к трансляции.");
			}
		});
	}
}
