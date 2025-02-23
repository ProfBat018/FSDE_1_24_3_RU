using System;
using System.Collections.Generic;

namespace Scaffolding;

public partial class Role
{
    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
