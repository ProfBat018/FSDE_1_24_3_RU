using Lesson10Interfaces.Interfaces;

namespace Lesson10Interfaces.Implementations;

class Car : ITransport
{
    public void Move()
    {
        Console.WriteLine("Car is moving");
    }
}