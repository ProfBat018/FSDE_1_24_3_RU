import React, { useRef } from 'react';
import { fetchMovies } from '../services/fetchService';

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



function SearchForm({setValue}) {

    const searchInput = useRef();
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        const res = await fetchMovies(searchInput.current.value);
        setValue(res);
    }

  

    return (
        <div className='m-5 h-12'>
            <form>
                <input className='w-96 h-[50px]' ref={searchInput} placeholder='Enter movie name' type='search' name="movie-name" id="movie-name" />
                <button onClick={(e) => handleSubmit(e)} type='submit'>Search</button>
            </form>
        </div>
    );
}
export default SearchForm;
