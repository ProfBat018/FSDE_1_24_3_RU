using System;
using System.Collections.Generic;

namespace DbFirst;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public string UserNameRef { get; set; } = null!;

    public string RoleNameRef { get; set; } = null!;

    public virtual Role RoleNameRefNavigation { get; set; } = null!;

    public virtual User UserNameRefNavigation { get; set; } = null!;
}
