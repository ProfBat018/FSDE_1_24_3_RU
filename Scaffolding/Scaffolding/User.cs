using System;
using System.Collections.Generic;

namespace Scaffolding;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsEmailConfirmed { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    
    public override string ToString()
    {
        return $"{UserName}\t{Email}\t{IsEmailConfirmed}";
    }
}
