//
// List<Person> people = new List<Person>()
// {
//     new()
//     {
//         Name = "Elvin",
//         Age = 23
//     },
//     new()
//     {
//         Name = "Samir",
//         Age = 29
//     },
//     new()
//     {
//         Name = "Nijat",
//         Age = 22
//     }
// };
//
// people.Sort();
//
// foreach (var person in people)
// {
//     Console.WriteLine(person.Name);
// }
//
//
// class Person : IComparable
// {
//     public string Name { get; set; }
//     public int Age { get; set; }
//     
//     
//     public int CompareTo(object? obj)
//     {
//         Person? otherPerson = obj as Person;
//         
//         if (otherPerson == null)
//         {
//             throw new ArgumentException("Object is not a Person");
//         }
//         
//         if (this.Age > otherPerson.Age)
//         {
//             return 1;
//         }
//         else if (this.Age < otherPerson.Age)
//         {
//             return -1;
//         }
//         else
//         {
//             return 0;
//         }
//     }
// }
//
//
//


// -------------------



class Person : IEquatable<Person>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public bool Equals(Person? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Age == other.Age;
    }
}