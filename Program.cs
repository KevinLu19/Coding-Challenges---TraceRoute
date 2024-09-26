using CodingChallenges_TraceRoute.Sockets;

namespace CodingChallenges_TraceRoute;

public class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("------------");
		Console.WriteLine("Traceroute command. Enter the host name for the traceroute command");
		Console.WriteLine("------------");

		//UDPSend udp_send = new UDPSend();


		_ = new Traceroute();

	}

}
