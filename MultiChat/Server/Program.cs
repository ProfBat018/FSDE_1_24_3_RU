using System.Net;
using System.Net.Sockets;
using System.Text;

var serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

var address = IPAddress.Parse("172.20.28.8"); // всегда localhost

var endPoint = new IPEndPoint(address, 3003);

var buffer = new byte[1024]; // делаю буферный массив для получения данных 

try
{
    serverSocket.Bind(endPoint);
    serverSocket.Listen();
    Console.WriteLine($"Listening on {endPoint.Address}:{endPoint.Port}");

    while (true)
    {
        var clientSocket = serverSocket.Accept();
        Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");

        // Передаем сокет клиенту в отдельный поток
        ThreadPool.QueueUserWorkItem(state =>
        {
            var socket = (Socket)state!;
            var buffer = new byte[1024];

            try
            {
                while (true)
                {
                    var bytesRead = socket.Receive(buffer);
                    if (bytesRead == 0) break;

                    var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.Write($"Thread: {Thread.CurrentThread.ManagedThreadId} works with {clientSocket.RemoteEndPoint}\n\t");

                    Console.WriteLine($"[{socket.RemoteEndPoint}] {message}");

                    if (message.Trim().ToLower() == "quit")
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Client disconnected: {socket.RemoteEndPoint}");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }, clientSocket);
    }
}
catch (Exception e)
{
    Console.WriteLine($"Server error: {e.Message}");
}
