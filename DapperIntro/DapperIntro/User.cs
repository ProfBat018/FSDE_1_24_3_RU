namespace DapperIntro;

public class User
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsEmailCondirmed  { get; set; }

    public override string ToString()
    {
        return $"{UserName}\t{Email}\t{Password}\t{IsEmailCondirmed}";
    }
}