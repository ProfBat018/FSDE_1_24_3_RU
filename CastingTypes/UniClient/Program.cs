using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            using var client = new TcpClient();
            client.Connect("172.20.208.84", 3003);

            using var networkStream = client.GetStream();
            using var writer = new StreamWriter(networkStream, Encoding.UTF8) { AutoFlush = true };

            while (true)
            {
                Console.WriteLine("Enter message to send to server or type 'quit' to exit:");
                string message = $"Test from client: {args[0]}";

                await writer.WriteLineAsync(message);
 
    
                
                await Task.Delay(1);

                if (message.ToLower() == "quit")
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Client error: {e.Message}");
        }
    }
}