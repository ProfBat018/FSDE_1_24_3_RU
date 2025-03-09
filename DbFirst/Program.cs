using DbFirst;

using var context = new Auth3Context();

// var userToAdd = new User()
// {
//     UserName = "user1",
//     Email = "aloha@gmail.com",
//     IsEmailConfirmed = true,
//     Password = BCrypt.Net.BCrypt.HashPassword("Aloha_123")
// };
//
// context.Users.Add(userToAdd);
// context.SaveChanges(); // если не сделать это то запись не добавится в базу
//
var users = context.Users.ToList();

foreach (User usr in users)
{
    Console.WriteLine($"{usr.UserName} {usr.IsEmailConfirmed}");
}


// var user = context.Users.FirstOrDefault(u => u.UserName == "user1");
//
// user.IsEmailConfirmed = false;
//
// context.SaveChanges();


