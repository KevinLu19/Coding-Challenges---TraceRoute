using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodingChallenges_TraceRoute;
public class Traceroute
{
    private string _hostname;
	private const int _MAX_NUM_HOPS = 30;
	private const string _packet_sending = "some random packet. mainly for testing the traceroute and not the packet itself.";
	public Traceroute(string hostname)
    {
        _hostname = hostname;

    }

    public void Print()
	{
        Console.WriteLine($"cctraceroute {_hostname}");
    }
}
