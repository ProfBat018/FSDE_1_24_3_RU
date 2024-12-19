Console.WriteLine("Start of Main");

void foo()
{
        int x = 0;
        int y = 10 / x;
}

try
{
    foo();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine("End of Main");