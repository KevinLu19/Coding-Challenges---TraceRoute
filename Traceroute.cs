using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace CodingChallenges_TraceRoute;
public class Traceroute : IDisposable
{
    private string _hostname;
    private readonly string _message = "Random message send to destination.";

    public Socket udp_socket;
    public Socket icmp_sock;

	public Traceroute()
    { 
        StartSocket();
    }

 //   public void PrintFirstLine()
	//{
 //       var max_hops = _udp_send.GetMaxHops();
 //       var packets = _udp_send.GetBytePacket();

 //       Console.WriteLine($"traceroute to {_hostname}, {max_hops} hops max, {packets} byte packets");
 //   }

    public void StartSocket()
    {
        IPAddress target_ip_add = IPAddress.Parse("8.8.8.8");
        int port = 33434;
        IPEndPoint endpoint = new(target_ip_add, port);


        // Scoket for UDP
        udp_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Send packet - the message.
        byte[] udp_pack = new byte[32];


		// Raw socket to listen for ICMP
		icmp_sock = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
		icmp_sock.ReceiveTimeout = 3000;            // 3 seconds

        int ttl = 1;        // Start with 1 for the first hop.
        udp_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, ttl);

        // Try to send message
        try
        {
            udp_socket.SendTo(udp_pack, endpoint);
            Console.WriteLine($"Packet send with ttl of {ttl}");

            // Prepare for icmp replies
            byte[] receive_buff = new byte[512];
            EndPoint remote_endpoint = new IPEndPoint(IPAddress.Any, 0);

            // Bind to any available local ip and port.
            icmp_sock.Bind(new IPEndPoint(IPAddress.Any, 0));

            // Wait for reply
            int receive_byte = icmp_sock.ReceiveFrom(receive_buff, ref remote_endpoint);
            Console.WriteLine("Reply receive from: " + remote_endpoint.ToString());

            // Parse reply
            IPAddress sender_ip = new IPAddress(receive_buff.Skip(12).Take(4).ToArray());
            Console.WriteLine("Reply Received from " + sender_ip);

            // cehcek icmp type
            int icmp_type = receive_buff[20];

            if (icmp_type == 11)        // Time exceed
            {
                Console.WriteLine("ICMP TYPE: Time Exceeded");
            }
            else if (icmp_type == 0)        // Echo reply - destination reached
            {
                Console.WriteLine("ICMP TYPE: Echo Reply");
            }

        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
    }

    public void Dispose()
    {
        udp_socket.Close();
        icmp_sock.Close();
    }
}
