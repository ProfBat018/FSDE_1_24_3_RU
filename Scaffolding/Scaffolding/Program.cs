using Microsoft.EntityFrameworkCore;
using Scaffolding;

using Auth3Context context = new(); 

var users = context.Users;

Console.WriteLine(users.ToQueryString());

foreach (var user in users)
{
    Console.WriteLine(user);
}

// context.Roles.Add(new Role { RoleName = "VISITOR" });
// context.SaveChanges();