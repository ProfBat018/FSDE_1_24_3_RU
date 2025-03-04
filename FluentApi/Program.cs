using FluentApi.Data.Contexts;
using FluentApi.Data.Models;

using var context = new ShowroomContext();

var newCarType = new CarType()
{
    CarTypeName = "SUV"
};

context.CarTypes.Add(newCarType);

context.SaveChanges();