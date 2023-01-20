using System;
using System.Collections.Generic;

namespace DesafioEncodeBDDO.Models;

public partial class DataUser
{
    public int IdDataUser { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? Dni { get; set; }
}
