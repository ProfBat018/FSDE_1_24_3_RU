using System.Dynamic;
using System.Runtime.Intrinsics.Arm;

namespace Aloha;

public class Car
{
    public string Make;
    public string Model;
    private protected string color;

    public static void Foo()
    {
        Console.WriteLine("Foo");
    }
}

public class GolfCar : Car
{
    GolfCar()
    {
        this.color = "Red";
    }
}
