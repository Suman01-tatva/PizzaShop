using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class OrderedItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int MenuItemId { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Amount { get; set; }

    public decimal? TotelModifierAmount { get; set; }

    public decimal? Tax { get; set; }

    public decimal TotelAmount { get; set; }

    public string? Instruction { get; set; }

    public int[]? OrderStatus { get; set; }

    public bool? IsDeleted { get; set; }

    public int? ReadyItemQuantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual MenuItem MenuItem { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<OrderedItemModifierMapping> OrderedItemModifierMappings { get; set; } = new List<OrderedItemModifierMapping>();
}
