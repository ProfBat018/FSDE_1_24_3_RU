
Console.WriteLine("Start of Program");

Console.WriteLine(A.Name);

static class A
{
    public static string Name { get; set; } = "A property";
    static A()
    {
        System.Console.WriteLine("static A constructor called");
    }
}


