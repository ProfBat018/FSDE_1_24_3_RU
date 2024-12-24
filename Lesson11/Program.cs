using Lesson11.Data.DTO.User;
using Lesson11.Implementations;
using Lesson11.Interfaces;


UserService userService = new UserService();


var user = userService.LoginUser(new Login_DTO("Elvin_123", "Elvin_1234"));

Console.WriteLine(user.Username);



    