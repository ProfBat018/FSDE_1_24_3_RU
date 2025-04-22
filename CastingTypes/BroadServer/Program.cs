// Program.cs

using System.Net;
using System.Net.Sockets;
using System.Text;


using UdpClient udpServer = new();
udpServer.EnableBroadcast = true;

IPEndPoint broadcastEndpoint = new(IPAddress.Broadcast, 11000);


Console.WriteLine($"Broadcast-сервер запущен на {broadcastEndpoint.Address}:{broadcastEndpoint.Port}. Нажмите Enter для отправки сообщений...");

while (true)
{
    Console.Write("Сообщение для отправки: ");
    string? message = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(message)) break;

    byte[] bytes = Encoding.UTF8.GetBytes(message);
    udpServer.Send(bytes, bytes.Length, broadcastEndpoint);

    Console.WriteLine("Сообщение отправлено всем клиентам.");
}