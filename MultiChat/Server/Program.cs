using System.Net;
using System.Net.Sockets;
using System.Text;

var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var address = IPAddress.Parse("127.0.0.1"); // всегда localhost

var endPoint = new IPEndPoint(address, 3003);

var buffer = new byte[1024]; // делаю буферный массив для получения данных 

try
{
    serverSocket.Bind(endPoint);

    serverSocket.Listen();
    Console.WriteLine($"Listening on {endPoint.Address}:{endPoint.Port}");

    while (true)
    {

        var clientSocket = serverSocket.Accept(); // принимаю клиента 
        Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");

        while (true)
        {
            var bytesRead = clientSocket.Receive(buffer); // получаю данные от клиента 
            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"{message}");

            if (message.ToLower() == "quit")
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                break;
            }
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e);
}