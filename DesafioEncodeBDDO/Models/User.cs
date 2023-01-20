using System;
using System.Collections.Generic;

namespace DesafioEncodeBDDO.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? NameUser { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }
}
