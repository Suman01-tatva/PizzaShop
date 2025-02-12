using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class MappingMenuItemsWithModifier
{
    public int Id { get; set; }

    public int MenuItemId { get; set; }

    public int ModifierGroupId { get; set; }

    public int[]? MinSelectionRequired { get; set; }

    public int? MaxSelectionAllowed { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual MenuItem MenuItem { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ModifierGroup ModifierGroup { get; set; } = null!;
}
