Console.Write("Enter your username: ");
string username = Console.ReadLine();

IDocument doc = new DocumentProxy(username);

doc.Display();
Console.WriteLine("Do something else...");
doc.Display(); // второй вызов не загрузит документ снова

