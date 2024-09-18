using CodingChallenges_TraceRoute.Sockets;

namespace CodingChallenges_TraceRoute;

public class Program
{
	private static void Main(string[] args)
	{
		//Console.WriteLine("Traceroute command. Enter the host name for the traceroute command");

		// string name = Console.ReadLine();

		//Traceroute rt = new Traceroute(name);

		//rt.Print();

		
		// Need to open 2 terminasl. One for server and the other for client.
		if (args.Length == 0)
		{
			Console.WriteLine("Please specify 'server' or 'client' as the first argument.");
			return;
		}

		if (args[0].ToLower() == "server")
		{
			Server socket_server = new Server();
		}
		else if (args[0].ToLower() == "client")
		{
			Client socket_client = new Client();
		}
		else
		{
			Console.WriteLine("Invalid argument. Use 'server' or 'client'.");
		}
	}

}