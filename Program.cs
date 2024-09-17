
public class Program
{
	private const int _MAX_NUM_HOPS = 30;
	private const string _packet_sending = "some random packet. mainly for testing the traceroute and not the packet itself.";
	private static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		Console.WriteLine("Traceroute command. Enter the host name for the traceroute command");
		var hostname = Console.Read();

		Console.WriteLine(hostname);
	}
}