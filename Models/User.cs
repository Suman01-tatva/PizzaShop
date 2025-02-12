using System;
using System.Collections.Generic;

namespace PizzaShop.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Username { get; set; } = null!;

    public long Phone { get; set; }

    public string? ProfileImg { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public string? Zipcode { get; set; }

    public string? Address { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public int CreatedBy { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Account> AccountCreatedByNavigations { get; set; } = new List<Account>();

    public virtual ICollection<Account> AccountModifiedByNavigations { get; set; } = new List<Account>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Customer> CustomerCreatedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Customer> CustomerModifiedByNavigations { get; set; } = new List<Customer>();

    public virtual ICollection<Feedback> FeedbackCreatedByNavigations { get; set; } = new List<Feedback>();

    public virtual ICollection<Feedback> FeedbackModifiedByNavigations { get; set; } = new List<Feedback>();

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseModifiedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<Invoice> InvoiceCreatedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<Invoice> InvoiceModifiedByNavigations { get; set; } = new List<Invoice>();

    public virtual ICollection<MappingMenuItemsWithModifier> MappingMenuItemsWithModifierCreatedByNavigations { get; set; } = new List<MappingMenuItemsWithModifier>();

    public virtual ICollection<MappingMenuItemsWithModifier> MappingMenuItemsWithModifierModifiedByNavigations { get; set; } = new List<MappingMenuItemsWithModifier>();

    public virtual ICollection<MenuCategory> MenuCategoryCreatedByNavigations { get; set; } = new List<MenuCategory>();

    public virtual ICollection<MenuCategory> MenuCategoryModifiedByNavigations { get; set; } = new List<MenuCategory>();

    public virtual ICollection<MenuItem> MenuItemCreatedByNavigations { get; set; } = new List<MenuItem>();

    public virtual ICollection<MenuItem> MenuItemModifiedByNavigations { get; set; } = new List<MenuItem>();

    public virtual User? ModifiedByNavigation { get; set; }

    public virtual ICollection<Modifier> ModifierCreatedByNavigations { get; set; } = new List<Modifier>();

    public virtual ICollection<ModifierGroup> ModifierGroupCreatedByNavigations { get; set; } = new List<ModifierGroup>();

    public virtual ICollection<ModifierGroup> ModifierGroupModifiedByNavigations { get; set; } = new List<ModifierGroup>();

    public virtual ICollection<Modifier> ModifierModifiedByNavigations { get; set; } = new List<Modifier>();

    public virtual ICollection<Order> OrderCreatedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderModifiedByNavigations { get; set; } = new List<Order>();

    public virtual ICollection<OrderTaxMapping> OrderTaxMappingCreatedByNavigations { get; set; } = new List<OrderTaxMapping>();

    public virtual ICollection<OrderTaxMapping> OrderTaxMappingModifiedByNavigations { get; set; } = new List<OrderTaxMapping>();

    public virtual ICollection<OrderedItem> OrderedItemCreatedByNavigations { get; set; } = new List<OrderedItem>();

    public virtual ICollection<OrderedItem> OrderedItemModifiedByNavigations { get; set; } = new List<OrderedItem>();

    public virtual ICollection<OrderedItemModifierMapping> OrderedItemModifierMappingCreatedByNavigations { get; set; } = new List<OrderedItemModifierMapping>();

    public virtual ICollection<OrderedItemModifierMapping> OrderedItemModifierMappingModifiedByNavigations { get; set; } = new List<OrderedItemModifierMapping>();

    public virtual ICollection<Payment> PaymentCreatedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Payment> PaymentModifiedByNavigations { get; set; } = new List<Payment>();

    public virtual ICollection<Permission> PermissionCreatedByNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<Permission> PermissionModifiedByNavigations { get; set; } = new List<Permission>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissionCreatedByNavigations { get; set; } = new List<RolePermission>();

    public virtual ICollection<RolePermission> RolePermissionModifiedByNavigations { get; set; } = new List<RolePermission>();

    public virtual ICollection<Section> SectionCreatedByNavigations { get; set; } = new List<Section>();

    public virtual ICollection<Section> SectionModifiedByNavigations { get; set; } = new List<Section>();

    public virtual ICollection<Table> TableCreatedByNavigations { get; set; } = new List<Table>();

    public virtual ICollection<Table> TableModifiedByNavigations { get; set; } = new List<Table>();

    public virtual ICollection<TableOrderMapping> TableOrderMappingCreatedByNavigations { get; set; } = new List<TableOrderMapping>();

    public virtual ICollection<TableOrderMapping> TableOrderMappingModifiedByNavigations { get; set; } = new List<TableOrderMapping>();

    public virtual ICollection<TaxesAndFee> TaxesAndFeeCreatedByNavigations { get; set; } = new List<TaxesAndFee>();

    public virtual ICollection<TaxesAndFee> TaxesAndFeeModifiedByNavigations { get; set; } = new List<TaxesAndFee>();

    public virtual ICollection<WaitingToken> WaitingTokenCreatedByNavigations { get; set; } = new List<WaitingToken>();

    public virtual ICollection<WaitingToken> WaitingTokenModifiedByNavigations { get; set; } = new List<WaitingToken>();
}
