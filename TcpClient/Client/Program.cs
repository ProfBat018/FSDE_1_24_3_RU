using System.Net;
using System.Net.Sockets;
using System.Text;

try
{
    using var client = new TcpClient();
    await client.ConnectAsync("172.20.28.8", 3003);

    using var networkStream = client.GetStream();
    using var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true };

    while (true)
    {
        Console.WriteLine("Enter message to send to server or type 'quit' to exit:");
        string message = Console.ReadLine();

        writer.WriteLineAsync(message);

        if (message.ToLower() == "quit")
            break;
    }
}
catch (Exception e)
{
    Console.WriteLine($"Client error: {e.Message}");
}