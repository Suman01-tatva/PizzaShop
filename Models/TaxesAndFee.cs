using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class TaxesAndFee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Type { get; set; }

    public decimal? FlatAmount { get; set; }

    public decimal? Percentage { get; set; }

    public decimal? TaxValue { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<OrderTaxMapping> OrderTaxMappings { get; set; } = new List<OrderTaxMapping>();
}
