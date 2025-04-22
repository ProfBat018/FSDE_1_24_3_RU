using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    public static async Task Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Please provide client name as first argument.");
            return;
        }


        using UdpClient udpClient = new UdpClient();
        IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("172.20.208.84"),3003);

        while (true)
        {
            Console.WriteLine("Enter message or 'quit' to exit:");
            string message = $"Test from client: {args[0]}";

            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await udpClient.SendAsync(buffer, buffer.Length, serverEP);

            if (message.ToLower() == "quit")
                break;

            await Task.Delay(1);
        }
    }
}