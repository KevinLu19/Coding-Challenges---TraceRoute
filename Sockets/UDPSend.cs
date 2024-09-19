using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CodingChallenges_TraceRoute.Sockets;

public interface IUDPSend
{
    public string GetHost();
    public int GetMaxHops();
    public void GetBytePacket();
}

public class UDPSend : IUDPSend
{
    private const int _MAX_HOPS = 30;
	private const string _random_sending_message = "Complete random string sending to server. Not important here.";
    private static string _host_name;

	public UDPSend()
    {
        StartServer();
    }

    public static void StartServer()
    {
        string dest = "8.8.8.8";
        int port = 80;
        int time_to_live = 1;

        _host_name = dest;
        
        // Send data using UDP
        using (Socket send_sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
			// Set TTL to 1
			send_sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, time_to_live);

            // Create Destination endpoint
            IPEndPoint dest_end = new IPEndPoint(IPAddress.Parse(dest), port);

            // Send message.
            byte[] message = Encoding.ASCII.GetBytes(_random_sending_message);

            send_sock.SendTo(message, dest_end);
		}

        // Receive ICMP message.
        using (Socket receive = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp))
        {
            // time out for icmp message
            receive.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);

            try
            {
                // save the message
                byte[] buff = new byte[1_024];
                EndPoint end_point = new IPEndPoint(IPAddress.Any, 0);

                // Receive
                int byte_receive = receive.ReceiveFrom(buff, ref end_point);

                // Extract ip
                IPEndPoint responder_end_point = end_point as IPEndPoint;

                if (responder_end_point != null)
                {
                    Console.WriteLine($"Received ICMP message from: {responder_end_point.Address}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Timeout with message. {ex}");
            }
        }
    }

	public string GetHost()
    {
        return _host_name;
    }
	public int GetMaxHops()
    {
        return _MAX_HOPS;
    }
	public void GetBytePacket()
    {

    }
}
