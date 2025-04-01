// Task foo()
// {
//     Console.WriteLine("Aloha");
//     
//     Thread.Sleep(1000);
//     return Task.CompletedTask;
// }
//
// Console.WriteLine("Start of Main");
// foo().Wait();
//
// Console.WriteLine("End of Main");


// Task<bool> isEven(int num1, int num2)
// {
//     return Task.FromResult(num1 % 2 == 0 && num2 % 2 == 0);
// }
//
// Console.WriteLine("Start of Main...");
// var res = isEven(1, 2).GetAwaiter().GetResult();
//
// Console.WriteLine(res);
//
// Console.WriteLine("End of Main...");


async Task<bool> isEven(int num1, int num2) => num1 % 2 == 0 && num2 % 2 == 0;

Console.WriteLine("Start of Main...");
var res = await isEven(1, 2);

Console.WriteLine(res);

Console.WriteLine("End of Main...");