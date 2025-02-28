using LibProj.Data.Contexts;
using Microsoft.EntityFrameworkCore;

using var context = new LibraryContext();

// get all Books 
//
// var books = context.Books
//     .Include(b => b.Author)
//     .ThenInclude(a => a.Person)
//     .Include(b => b.Publisher)
//     .Include(b => b.Editor)
//     .ThenInclude(e => e.Person)
//     .Select(b => new
//     {
//         BookName = b.Name,
//         AuthorName = b.Author.Person.Name,
//         AuthorSurname = b.Author.Person.Surname,
//         EditorName = b.Editor.Person.Name,
//         EditorSurname = b.Editor.Person.Surname,
//         Publisher = b.Publisher.Name
//     });
//
// foreach (var book in books)
// {
//     Console.WriteLine($"{book.BookName} - {book.AuthorName} {book.AuthorSurname} - {book.EditorName} {book.EditorSurname} - {book.Publisher}");;
// }

// Explicit Loading

/*

// var book = context.Books.First();
//
// context.Entry(book)
//     .Reference(b => b.Author)
//     .Load();
//
// context.Entry(book.Author)
//     .Reference(a => a.Person)
//     .Load();
//
// Console.WriteLine(book.Author.Person.Name);
*/


// Взял одного человека, который является автором
var person = context.People.Skip(3).Take(1).First();

// Загрузил данные автора, который является этот человек
var author = context.Entry(person)
    .Collection(p => p.Authors)
    .Query().First();


context.Entry(author)
    .Collection(a => a.Books)
    .Load();

// var books  = context.Entry(author)
//     .Collection(a => a.Books)
//     .Query().ToList();

foreach (var book in author.Books)
{
    Console.WriteLine($"{book.Name}");
}