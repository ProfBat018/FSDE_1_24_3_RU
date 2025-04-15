public class SecretDocument : IDocument
{
    public SecretDocument()
    {
        Console.WriteLine("Loading Secret Document from DB...");
        Thread.Sleep(1000); // симуляция ресурсоёмкой операции
    }

    public void Display()
    {
        Console.WriteLine("Displaying secret document content...");
    }
}