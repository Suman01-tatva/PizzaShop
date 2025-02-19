using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
