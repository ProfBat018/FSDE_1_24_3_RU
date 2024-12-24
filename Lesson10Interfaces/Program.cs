using Lesson10Interfaces.Implementations;
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

class Rifle : IGun
{
    public void Shoot()
    {
        Console.WriteLine("Rifle is shooting");
    }
}





