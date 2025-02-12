using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual ICollection<Modifier> Modifiers { get; set; } = new List<Modifier>();
}
