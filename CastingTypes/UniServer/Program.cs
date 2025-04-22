using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener listener = new(IPAddress.Any, 3003);
listener.Start();

Console.WriteLine("Server started. Waiting for connections...");

while (true)
{
    try
    {
        TcpClient client = await listener.AcceptTcpClientAsync();
        HandleClientAsync(client); // fire and forget
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error accepting client: {ex.Message}");
    }
}

static async Task HandleClientAsync(TcpClient client)
{
    var endPoint = client.Client.RemoteEndPoint;
    Console.WriteLine($"Client connected: {endPoint} on thread {Thread.CurrentThread.ManagedThreadId}");

    try
    {
        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new(stream, Encoding.UTF8);

        while (true)
        {
            string? message = await reader.ReadLineAsync();

            if (message == null)
            {
                Console.WriteLine($"Client {endPoint} disconnected.");
                break;
            }

            Console.WriteLine($"Received from {endPoint}: {message} with thread {Thread.CurrentThread.ManagedThreadId}");

            if (message.ToLower() == "quit")
            {
                Console.WriteLine($"Client {endPoint} requested to quit.");
                break;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Client error ({endPoint}): {ex.Message}");
    }
    finally
    {
        client.Close();
    }
}