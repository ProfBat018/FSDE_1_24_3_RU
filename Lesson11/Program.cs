using Lesson11;
using Lesson11.Implementations;
using Lesson11.Interfaces;

Menu menu = new();
IMovieService movieService = new MovieService();

menu.DisplayMenu();

MenuChoice choice = menu.GetMenuChoice();

bool flag = true;
while (flag)
{
    switch (choice.Id)
    {
        case 1:
            Console.WriteLine($"You chose {choice.Description}");

            Console.WriteLine("Enter movie name:");
            var movieName = Console.ReadLine();
            
            var res = movieService.SearchMovie(movieName);

            Console.WriteLine(res);

            foreach (var movie in res.results)
            {
                Console.WriteLine(movie);
            }
            
            break;
        case 2:
            Console.WriteLine($"You chose {choice.Description}");
            break;
        case 3:
            flag = false;
            Console.WriteLine("Exit");
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}

Console.WriteLine("Goodbye!");