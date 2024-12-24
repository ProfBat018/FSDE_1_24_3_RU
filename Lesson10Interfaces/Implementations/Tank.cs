using Lesson10Interfaces.Interfaces;

namespace Lesson10Interfaces.Implementations;

class Tank : ITransport, IGun
{
    public void Move()
    {
        Console.WriteLine("Tanl is moving");
    }

    public void Shoot()
    {
        Console.WriteLine("Tank is shooting");
    }
}