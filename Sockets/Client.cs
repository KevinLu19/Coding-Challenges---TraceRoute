using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenges_TraceRoute.Sockets;
public class Client
{
    private const string _random_sending_message = "Complete random string sending to server. Not important here.";

    public Client()
    {
        StartClient();
    }

    public static void StartClient()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 8080);

            byte[] data = Encoding.UTF8.GetBytes(_random_sending_message);

            // Sending message
            NetworkStream steam = client.GetStream();
            steam.Write(data, 0, data.Length);
            Console.WriteLine($"Sent: {_random_sending_message}");

            byte[] response_buff = new byte[1_024];
            int byte_read = steam.Read(response_buff, 0, response_buff.Length);

            string response = Encoding.UTF8.GetString(response_buff, 0, byte_read);
            Console.WriteLine($"Received: {response}");

            steam.Close();
            client.Close();
        }
        catch (SocketException e)
        {
            Console.WriteLine($"Socket exception: {e}");
        }
    }
}
