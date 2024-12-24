using System.Text;
using System.Text.Json;
using Lesson11.Data.DTO.User;
using Lesson11.Data.Model;
using Lesson11.Interfaces;

namespace Lesson11.Implementations;

public class UserService : IUserService
{
    public List<User> Users { get; set; } =
        new(); // если не сделаю то будет object reference not set to an instance of an object

    public User LoginUser(Login_DTO loginDto)
    {
        if (!ValidateService.ValidateLogin(loginDto))
            throw new Exception("Invalid login credentials");

        var json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
        {
            Users = JsonSerializer.Deserialize<List<User>>(json);

            foreach (var user in Users)
            {
                if (user.Username == loginDto.username && user.Password == loginDto.password)
                {
                    Console.WriteLine("User logged in successfully");
                    return user;
                }
            }
            throw new Exception("Invalid login credentials");
        }
        throw new Exception("No users found");
    }

    public void RegisterUser(Register_DTO registerDto)
    {
        if (!ValidateService.ValidateRegister(registerDto))
            throw new Exception("Invalid registration credentials");

        var json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
        {
            Users = JsonSerializer.Deserialize<List<User>>(json);
        }

        Users.Add(new User()
        {
            Username = registerDto.username,
            Password = registerDto.password,
            Email = registerDto.email,
            DateOfBirth = registerDto.dateOfBirth
        });

        var jsonString = JsonSerializer.Serialize(Users);

        File.WriteAllText("./Data/Users.json", jsonString);

        Console.WriteLine("User registered successfully");
    }

    public void ForgotPassword(Forgot_DTO forgotDto)
    {
        throw new NotImplementedException();
    }
}