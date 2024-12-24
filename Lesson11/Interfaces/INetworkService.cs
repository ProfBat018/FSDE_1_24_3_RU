namespace Lesson11.Interfaces;

public interface INetworkService
{
    public string GetDataFromNetwork(string url, HttpMethod method);
}