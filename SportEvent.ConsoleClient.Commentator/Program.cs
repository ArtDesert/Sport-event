namespace SportEvent.ConsoleClient.Commentator;

public class Program
{
	public static void Main(string[] args)
	{
        var commentator = new CommentatorClient();
		try
		{
			commentator.Run();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Не удалось подключиться к серверу. {ex.Message}");
        }
	}
}
