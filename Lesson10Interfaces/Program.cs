using Lesson10Interfaces.Interfaces;


ITransport[] transports = new ITransport[2]
{
    new Tank(),
    new Car()
};

IGun[] guns = new IGun[2]
{
    new Tank(),
    new Rifle()
};

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

class Car : ITransport
{
    public void Move()
    {
        Console.WriteLine("Car is moving");
    }
}

class Rifle : IGun
{
    public void Shoot()
    {
        Console.WriteLine("Rifle is shooting");
    }
}





