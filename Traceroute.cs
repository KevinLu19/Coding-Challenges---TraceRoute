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
	public Traceroute(string hostname)
    {
        _hostname = hostname;
    }

    public void PrintFirstLine()
	{
        Console.WriteLine($"traceroute to <host>, <max hops>, <byte packets>");
    }
}
