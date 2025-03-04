using System.Runtime.InteropServices;
using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;

// using var context = new ShowroomContext();

#region Part1


// var fuelTypes = new List<FuelType>()
// {
//     new() { FuelTypeName = "Diesel" },
//     new() { FuelTypeName = "Petrol" },
//     new() { FuelTypeName = "Electric" },
//     new() { FuelTypeName = "Hybrid Petrol" },
//     new() { FuelTypeName = "Hybrid Diesel" },
//     new() { FuelTypeName = "LPG" },
//     new() { FuelTypeName = "CNG" },
//     new() { FuelTypeName = "Hydrogen" }
// };
//
// context.FuelTypes.AddRange(fuelTypes);
//
// context.SaveChanges();
#endregion

#region Part2

// var carTypes = new List<CarType>()
// {
//     new() { CarTypeName = "Sedan" },
//     new() { CarTypeName = "Coupe" },
//     new() { CarTypeName = "Hatchback" },
//     new() { CarTypeName = "SUV" },
//     new() { CarTypeName = "Crossover" },
//     new() { CarTypeName = "Convertible" }
// };
//
// context.CarTypes.AddRange(carTypes);
//
// context.SaveChanges();
//

#endregion

#region Part3
/*
var cars = new List<Car>()
{
    new()
    {
        Make = "BMW",
        Model = "M3",
        Color = "Sophisto Grey",
        ProductionDate = new DateTime(2021, 1, 1),
        CarTypeId = 1,
        FuelTypeId = 2
    },
    new()
    {
        Make = "Mercedes-Benz",
        Model = "C-Class",
        Color = "Black",
        ProductionDate = new DateTime(2021, 1, 1),
        CarTypeId = 1,
        FuelTypeId = 1
    },
    new()
    {
        Make = "Cadillac",
        Model = "Escalade",
        Color = "Black",
        ProductionDate = new DateTime(2021, 1, 1),
        CarTypeId = 4,
        FuelTypeId = 2
    }
};

context.Cars.AddRange(cars);

context.SaveChanges();

*/


#endregion

#region Part4

// var cars = context.Cars.ToList();
//
// foreach (var car in cars)
// {
//     Console.WriteLine($"Make: {car.Make}");
// }

// --------------------------------------------------


// var cars = context.Cars;
//
// foreach (var car in cars)
// {
//     Console.WriteLine($"Make: {car.Make}");
// }
//


#endregion

#region Part5

// var cars = context.Cars.Select(x => x.Make);
//
// Console.WriteLine(cars.ToQueryString());
//
// foreach (var car in cars)
// {
//     Console.WriteLine($"Make: {car}");
// }



#endregion

#region Part6
/*
var cars = context.Cars
    .Include(c => c.CarType)
    .Include(c => c.FuelType)
    .Where(c => c.FuelType.FuelTypeName == "Diesel")
    .Select(c => new {c.Make, c.Model, c.FuelType.FuelTypeName, c.CarType.CarTypeName});

Console.WriteLine(cars.ToQueryString());

foreach (var car in cars)
{
    Console.WriteLine($"{car.Make}\t{car.Model}\t{car.CarTypeName}\t{car.FuelTypeName}");
}
*/
#endregion

#region Part7

/*
var cars = context.Cars
    .Where(c => c.FuelType.FuelTypeName == "Diesel")
    .Select(c => new {c.Make, c.Model, c.FuelType.FuelTypeName, c.CarType.CarTypeName});

Console.WriteLine(cars.ToQueryString());

foreach (var car in cars)
{
    Console.WriteLine($"{car.Make}\t{car.Model}\t{car.CarTypeName}\t{car.FuelTypeName}");
}

*/
#endregion

#region Part8

using var context = new ShowroomContext();

var cars = context.Cars
    .Include(c => c.FuelType)
    .Include(c => c.CarType)
    .Select(c => new {c.Make, c.Model, c.CarType.CarTypeName, c.FuelType.FuelTypeName});

Console.WriteLine(cars.ToQueryString());


#endregion
