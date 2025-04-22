using System.Net;
using System.Net.Sockets;
using System.Text;


int MulticastPort = 3003;
string MulticastGroupAddress = "239.0.0.222";

using var udpClient = new UdpClient();

/*
 в данном случае я настраиваю сокет для мультикастовой рассылки.
 чтобы он мог принимать сообщения от других сокетов, которые также используют мультикаст. 
 ReuseAddress позволяет использовать один и тот же адрес для нескольких сокетов. 
 потому что разные клиенты будут использовать один и тот же адрес для получения сообщений.
*/
udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, MulticastPort));

// говорю сокету, что он будет принимать сообщения от мультикастовой группы 
udpClient.JoinMulticastGroup(IPAddress.Parse(MulticastGroupAddress));

Console.WriteLine("Multicast Client запущен. Ожидание сообщений...");

while (true)
{
    byte[] buffer = new byte[1024];
    IPEndPoint remoteEndPoint = new(IPAddress.Any, 0);
    buffer = udpClient.Receive(ref remoteEndPoint);
    string message = Encoding.UTF8.GetString(buffer);

    Console.WriteLine($"[Получено от {remoteEndPoint}]: {message}");
}


