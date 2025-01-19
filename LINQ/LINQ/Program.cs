#region Example1

// List<int> nums = new() { 1, 2, 3, 4, 5 };

// nums.ForEach(i => Console.WriteLine(i));

// var evenNums = new List<int>(nums.Where(x => x % 2 == 0)); // nums.Where(x => x % 2 == 0).ToList();
// evenNums.ForEach(i => Console.WriteLine(i));

#endregion

#region Example2

// List<Person> people = new()
// {
//     new Person { Name = "John", Surname = "Doe", Age = 25 },
//     new Person { Name = "Jane", Surname = "Doe", Age = 30 },
//     new Person { Name = "Jack", Surname = "Doe", Age = 35 },
//     new Person { Name = "Jill", Surname = "Doe", Age = 40 }
// };
//
// people.Where(p => p.Age > 30).Select(p => p.Name).ToList().ForEach(item => Console.WriteLine(item));

// people.Where(p => p.Age > 30).Select(p => new {p.Name, p.Age}).ToList().ForEach(item =>
// {
//     Console.WriteLine($"Name: {item.Name}, Age: {item.Age}");
// });

// class Person
// {
//     public string Name { get; set; }
//     public string Surname { get; set; }
//     public int Age { get; set; }
// }

#endregion




class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public DateTime Year { get; set; }
}

class Showroom
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public List<Car> Cars { get; set; }
    public int CarCapacity { get; set; } // машин не может быть больше чем CarCapacity
    public int CarCount => Cars.Count; // количество машин в салоне
    public int SalesCount { get; set; }
}

class User
{
    // автоматически генерируется новый Guid для уникальности
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShowroomId { get; set; } // идентификатор салона в котором работает пользователь
    public string Username { get; set; }
    public string Password { get; set; }
}

class Sale
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShowroomId { get; set; } // идентификатор салона в котором произошла продажа
    public Guid CarId { get; set; } // идентификатор машины которая была продана
    public Guid UserId { get; set; } // идентификатор пользователя который продал машину
    public DateTime SaleDate { get; set; } // дата продажи
}


    

