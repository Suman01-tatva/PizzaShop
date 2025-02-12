using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class City
{
    public int Id { get; set; }

    public int StateId { get; set; }

    public string Name { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
