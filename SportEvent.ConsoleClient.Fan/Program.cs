namespace SportEvent.ConsoleClient.Fan;

public class Program
{
	public static void Main(string[] args)
	{
		var fan = new FanClient();
		try
		{
			fan.Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Не удалось подключиться к серверу. {ex.Message}");
		}
	}
}
