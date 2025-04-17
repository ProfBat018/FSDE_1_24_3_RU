using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener listener = new(IPAddress.Any, 3003);
listener.Start();

Console.WriteLine("Server started. Waiting for a connection...");

while (true)
{
    using TcpClient client = await listener.AcceptTcpClientAsync();
    Console.WriteLine("Client connected.");

    using NetworkStream stream = client.GetStream();
    using var reader = new StreamReader(stream, Encoding.UTF8);

    while (true)
    {
        string? message = await reader.ReadLineAsync();

        if (message == null)
            break;

        Console.WriteLine($"Received: {message}");

        if (message.ToLower() == "quit")
            break;
    }
}