using Lesson11.Data.Model;

namespace Lesson11.Interfaces;

public interface IMovieService
{
    public MovieSearchResult? SearchMovie(string movieName, int page = 1);
}