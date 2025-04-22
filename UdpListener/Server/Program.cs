using System.Net;
using System.Net.Sockets;
using System.Text;

const int listenPort = 3003;

using UdpClient udpServer = new UdpClient(listenPort);
Console.WriteLine($"UDP Server started on port {listenPort}");

while (true)
{
    try
    {
        var received = await udpServer.ReceiveAsync();
        string message = Encoding.UTF8.GetString(received.Buffer);
        Console.WriteLine($"Received from {received.RemoteEndPoint}: {message}");

        if (message.ToLower() == "quit")
        {
            Console.WriteLine("Client requested to quit.");
            break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}