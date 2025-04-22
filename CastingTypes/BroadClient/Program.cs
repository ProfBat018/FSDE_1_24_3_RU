// Program.cs

using System.Net;
using System.Net.Sockets;
using System.Text;

using UdpClient udpClient = new(11000); // тот же порт, что и сервер
IPEndPoint remoteEP = new(IPAddress.Any, 0);

Console.WriteLine("Клиент запущен и слушает broadcast на порту 11000...");

while (true)
{
    byte[] bytes = udpClient.Receive(ref remoteEP);
    string message = Encoding.UTF8.GetString(bytes);

    Console.WriteLine($"Получено сообщение от {remoteEP}: {message}");
}