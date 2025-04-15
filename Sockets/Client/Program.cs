using System.Net;
using System.Net.Sockets;

var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var address = IPAddress.Parse("127.0.0.1");
var serverEndPoint = new IPEndPoint(address, 3003);

try
{
    clientSocket.Connect(serverEndPoint);
    
    while (true)
    {
        Console.WriteLine("Enter message to send to server or type 'quit' to exit:");
        string message = Console.ReadLine();

        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
        clientSocket.Send(messageBytes);
        Console.WriteLine($"Sent message: {message}");

        if (message.ToLower() == "quit")
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
            break;
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}