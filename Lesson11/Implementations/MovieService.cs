using System.Text;
using System.Text.Json;
using Lesson11.Data.Model;
using Lesson11.Interfaces;

namespace Lesson11.Implementations;

public class MovieService : IMovieService
{
    public MovieSearchResult? SearchMovie(string movieName, int page = 1)
    {
        // Создаю класс HttpClient для отправки запроса
        var client = new HttpClient();

        // Создаю объект HttpRequestMessage для отправки запроса
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri =
                new Uri(
                    $"https://api.themoviedb.org/3/search/movie?query={movieName}&include_adult=false&language=en-US&page={page}"),
            Headers =
            {
                { "accept", "application/json" },
                {
                    "Authorization",
                    "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyYTcxYWMxNTc3NzdkZTM3YzIxNTFjY2Q3OTQxZjU1YSIsIm5iZiI6MTY5Nzc4NDY2OS4yMDgsInN1YiI6IjY1MzIyMzVkOWFjNTM1MDg3NzU2MGEzYyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.hFRAfYIZ3c589bcPOw8gDGN_fPWT1BZnimjUxlbYa3I"
                },
            },
        };

        // Отправляю запрос и получаю ответ

        var response = client.Send(request); // Отправляю запрос и получаю ответ

        response.EnsureSuccessStatusCode(); // Проверяю успешность запроса

        
        var responseStream = response.Content.ReadAsStream(); // Считываю ответ в поток

        byte[] buffer = new byte[responseStream.Length]; // создал временный буфер для чтения потока

        responseStream.Read(buffer, 0, buffer.Length); // читаю поток в буфер
        
        // Encoding - это встроенный класс, который позволяет работать с кодировками
        var json = Encoding.UTF8.GetString(buffer); 
        
        return JsonSerializer.Deserialize<MovieSearchResult>(json);
    }
}