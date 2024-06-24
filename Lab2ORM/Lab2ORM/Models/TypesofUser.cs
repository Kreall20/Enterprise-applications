using System;
using System.Collections.Generic;

namespace Lab2ORM.Models;

public partial class TypesofUser
{
    public int Typeid { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
