#region Example1

// List<int> nums = new() { 1, 2, 3, 4, 5 };

// nums.ForEach(i => Console.WriteLine(i));

// var evenNums = new List<int>(nums.Where(x => x % 2 == 0)); // nums.Where(x => x % 2 == 0).ToList();
// evenNums.ForEach(i => Console.WriteLine(i));

#endregion

#region Example2

List<Person> people = new()
{
    new Person { Name = "John", Surname = "Doe", Age = 25 },
    new Person { Name = "Jane", Surname = "Doe", Age = 30 },
    new Person { Name = "Jack", Surname = "Doe", Age = 35 },
    new Person { Name = "Jill", Surname = "Doe", Age = 40 }
};

people.Where(p => p.Age > 30).Select(p => p.Name).ToList().ForEach(item => Console.WriteLine(item));

// people.Where(p => p.Age > 30).Select(p => new {p.Name, p.Age}).ToList().ForEach(item =>
// {
//     Console.WriteLine($"Name: {item.Name}, Age: {item.Age}");
// });

class Person
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
}

#endregion






