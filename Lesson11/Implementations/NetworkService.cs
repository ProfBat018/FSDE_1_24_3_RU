using Lesson11.Interfaces;

namespace Lesson11.Implementations;

public class NetworkService : INetworkService
{
    private readonly HttpClient _httpClient = new();
    
    public string GetDataFromNetwork(string url, HttpMethod method)
    {
        throw new NotImplementedException();
    }
}