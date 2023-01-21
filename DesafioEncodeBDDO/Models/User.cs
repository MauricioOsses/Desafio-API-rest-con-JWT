using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioEncodeBDDO.Models;

public partial class User
{
    [Key]
    public int IdUser { get; set; }

    public string? NameUser { get; set; }

    public string? Password { get; set; }

    public string? Rol { get; set; }
}
