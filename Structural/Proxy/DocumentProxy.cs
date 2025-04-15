public class DocumentProxy : IDocument
{
    private SecretDocument? _realDocument;
    private readonly string _username;

    public DocumentProxy(string username)
    {
        _username = username;
    }

    public void Display()
    {
        if (_username != "admin")
        {
            Console.WriteLine("Access Denied: Only admin can view the document.");
            return;
        }

        _realDocument ??= new SecretDocument();
        _realDocument.Display();
    }
}