using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorFirst.Models;
using RazorFirst.Services;

namespace RazorFirst.Pages;

public class Index : PageModel
{
    private readonly SearchService? _searchService;
    public SearchResult SearchResult { get; set; }

    public Index(SearchService searchService)
    {
        _searchService = searchService;
    }

    public void OnGet()
    {
        
    }

    public async Task OnPostAsync()
    {
        var movieName = Request.Form["movieInput"];
        var json = await _searchService.GetMoviesAsync(movieName);

        SearchResult = JsonSerializer.Deserialize<SearchResult>(json);

        Console.WriteLine(SearchResult.TotalResults);
    }
}