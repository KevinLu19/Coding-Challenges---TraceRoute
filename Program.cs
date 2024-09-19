using CodingChallenges_TraceRoute.Sockets;

namespace CodingChallenges_TraceRoute;

public class Program
{
	private static void Main(string[] args)
	{
		//Console.WriteLine("Traceroute command. Enter the host name for the traceroute command");

		UDPSend udp_send = new UDPSend();

		string name = Console.ReadLine();
		Traceroute rt = new Traceroute(name);

		rt.PrintFirstLine();
	}

}