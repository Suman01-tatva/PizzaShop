using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class Invoice
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ModifierId { get; set; }

    public int? QuantityOfModifier { get; set; }

    public decimal? RateOfModifier { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Modifier Modifier { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
