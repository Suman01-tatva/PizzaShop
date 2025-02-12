using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class RolePermission
{
    public int Id { get; set; }

    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    public bool? CanView { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual Role Role { get; set; } = null!;
}
