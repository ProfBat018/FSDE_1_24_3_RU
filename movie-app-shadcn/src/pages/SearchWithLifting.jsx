import React, { useEffect, useMemo, useState } from 'react';
import SearchForm from '../components/SearchForm';
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from '@/components/ui/card';
import { Button } from '@/components/ui/button';
import { Pagination, PaginationContent, PaginationItem, PaginationNext, PaginationPrevious } from '@/components/ui/pagination';


function SearchWithLifting() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    console.log('movies', movies);
  }, [movies]);
    

  const movieCards = useMemo(() => {
    if (!movies.results) return null;

    return movies.results.map((m) => (
      <Card key={m.id} className="w-72">
        <div className="relative h-56 overflow-hidden rounded-t-md">
          <img
            src="https://images.unsplash.com/photo-1540553016722-983e48a2cd10?ixlib=rb-1.2.1&auto=format&fit=crop&w=800&q=80"
            alt="card-image"
            className="object-cover w-full h-full"
          />
        </div>
        <CardHeader>
          <CardTitle className="text-xl">{m.title}</CardTitle>
          <CardDescription>{m.overview?.slice(0, 120)}...</CardDescription>
        </CardHeader>
        <CardFooter>
          <Button>Read more</Button>
        </CardFooter>
      </Card>
    ));
  }, [movies]);

  return (
    <div className="flex flex-col items-center justify-center space-y-6 px-4 py-8">
      <SearchForm setValue={setMovies} />
      <div className="flex flex-wrap justify-center gap-6">
        {movieCards}
      </div>
      <Pagination>
  <PaginationContent>
    <PaginationItem>
      <PaginationPrevious href="#" />
    </PaginationItem>
    {movies?.total_pages > 1 && Array.from({ length: movies.total_pages }, (_, index) => (
        <PaginationItem key={index}>
            <a href="#" className="px-3 py-2">
            {index + 1}
            </a>
        </PaginationItem>
        ))}
    <PaginationItem>
      <PaginationNext href="#" />
    </PaginationItem>
  </PaginationContent>
</Pagination>
    </div>
  );
}

export default SearchWithLifting;
