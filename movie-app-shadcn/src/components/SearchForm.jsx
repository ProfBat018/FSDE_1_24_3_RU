import React, { useRef } from 'react';

// function SearchForm({onSendData}) {

//     const searchInput = useRef();

//     const handleSubmit = (e) => {
//         e.preventDefault();
//         fetchMovies(searchInput.current.value);
//     }

//     const fetchMovies = (movieName) => {

//         const url = `https://api.themoviedb.org/3/search/movie?query=${movieName}&include_adult=false&language=en-US&page=1`;
//         const options = {
//             method: 'GET',
//             headers: {
//                 accept: 'application/json',
//                 Authorization: 'Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyYTcxYWMxNTc3NzdkZTM3YzIxNTFjY2Q3OTQxZjU1YSIsIm5iZiI6MTY5Nzc4NDY2OS4yMDgsInN1YiI6IjY1MzIyMzVkOWFjNTM1MDg3NzU2MGEzYyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.hFRAfYIZ3c589bcPOw8gDGN_fPWT1BZnimjUxlbYa3I'
//             }
//         };

//         fetch(url, options)
//             .then(res => res.json())
//             .then(json =>
//                 {
//                     console.log(json);
//                     onSendData(json);
//                 })
//             .catch(err => console.error(err));

//     }

//     return (
//         <div className='m-5 h-12'>
//             <form>
//                 <input className='w-96 h-[50px]' ref={searchInput} placeholder='Enter movie name' type='search' name="movie-name" id="movie-name" />
//                 <button onClick={(e) => handleSubmit(e)} type='submit'>Search</button>
//             </form>
//         </div>
//     );
// }
// export default SearchForm;



import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import useSWR from 'swr';
import { fetcher } from '../services/fetchService';

function SearchForm({ setValue }) {
    const searchInput = useRef(null);

    const handleSubmit = (e) => {
        e.preventDefault();
        setValue(data);
    };


    const { data } = useSWR(
        {
            movieName: searchInput?.current?.value,
            page: 1
        }, fetcher);

    return (
        <form onSubmit={handleSubmit} className="flex items-center space-x-4 w-full max-w-xl">
            <Input
                ref={searchInput}
                placeholder="Enter movie name"
                type="search"
                name="movie-name"
                id="movie-name"
                className="flex-grow"
            />
            <Button type="submit">Search</Button>
        </form>
    );
}

export default SearchForm;
