using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CodingChallenges_TraceRoute.Sockets;

public interface IPrintTraceroute
{
    void PrintTrace(string host, int max_hop, byte packet_byte);
}

public class Server : IPrintTraceroute
{
    private const int _MAX_HOPS = 30;

    public Server()
    {
        StartServer();
    }

    public static void StartServer()
    {
        IPAddress ip_address = IPAddress.Parse("127.0.0.1");
        int port = 8080;

        TcpListener listen = new TcpListener(ip_address, port);
        listen.Start();
        Console.WriteLine($"Server started on {ip_address} on port {port}");

        while (true)
        {
            // Listen for a connection.
            Console.WriteLine("Waiting for a connection...");
            TcpClient client = listen.AcceptTcpClient();
            Console.WriteLine("Connected");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1_024];
            int bytes_read = stream.Read(buffer, 0, buffer.Length);

            // Receive the message.
            string received_message = Encoding.UTF8.GetString(buffer, 0, bytes_read);
            Console.WriteLine($"Received: {received_message}");

            byte[] response = Encoding.UTF8.GetBytes("Message received by server.");
            stream.Write(response, 0, response.Length);

            client.Close();
        }
    }

    public void PrintTrace(string host, int max_hop, byte packet_byte)
    {
        Console.WriteLine($"traceroute to {host}, {max_hop} hops max, {packet_byte} byte packets");
    }
}
