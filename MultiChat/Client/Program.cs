using System.Net;
using System.Net.Sockets;
using System.Text;

var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var address = IPAddress.Parse("172.20.28.8");
var serverEndPoint = new IPEndPoint(address, 3003);

Console.Write("Enter your username: ");
StringBuilder sb = new(Console.ReadLine());
sb.Append(": ");

var usernameBytes = Encoding.UTF8.GetBytes(sb.ToString());

try
{
    clientSocket.Connect(serverEndPoint);

    while (true)
    {
        Console.WriteLine("Enter message to send to server or type 'quit' to exit:");

        string message = Console.ReadLine();


        var messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] finalMessage = new byte[usernameBytes.Length + messageBytes.Length];

        Buffer.BlockCopy(usernameBytes, 0, finalMessage, 0, usernameBytes.Length);
        Buffer.BlockCopy(messageBytes, 0, finalMessage, usernameBytes.Length, messageBytes.Length);

        clientSocket.Send(finalMessage);

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