using System.Xml;

namespace Lesson11;

public class Menu
{
    public List<MenuChoice> MenuChoices = new()
    {
        new() { Id = 1, Description = "Search for a movie by name" },
        new() { Id = 2, Description = "Search for a movie by genre" },
        new() { Id = 3, Description = "Exit" },
    };
    
    public void DisplayMenu()
    {
        Console.WriteLine("Please choose an option:");
        foreach (var choice in MenuChoices)
        {
            Console.WriteLine($"{choice.Id}. {choice.Description}");
        }
    }
    
    public MenuChoice GetMenuChoice()
    {
        var choice = Console.ReadLine();
        if (int.TryParse(choice, out var result))
        {
            return MenuChoices[result - 1];
        }
        return MenuChoices[2];
    }
}

public class MenuChoice
{
    public  int Id { get; set; }
    public string Description { get; set; }
}