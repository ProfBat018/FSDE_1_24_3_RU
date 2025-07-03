export const fetchMovies = async (movieName) => {

    const url = `https://api.themoviedb.org/3/search/movie?query=${movieName}&include_adult=false&language=en-US&page=1`;
    const options = {
        method: 'GET',
        headers: {
            accept: 'application/json',
            Authorization: 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyYTcxYWMxNTc3NzdkZTM3YzIxNTFjY2Q3OTQxZjU1YSIsIm5iZiI6MTY5Nzc4NDY2OS4yMDgsInN1YiI6IjY1MzIyMzVkOWFjNTM1MDg3NzU2MGEzYyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.hFRAfYIZ3c589bcPOw8gDGN_fPWT1BZnimjUxlbYa3I'
        }
    };


    const data = await fetch(url, options);

    const json = await data.json();

    return json;
        
}