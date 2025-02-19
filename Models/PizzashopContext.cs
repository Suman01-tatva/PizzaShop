using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models;

public partial class PizzashopContext : DbContext
{
    public PizzashopContext(DbContextOptions<PizzashopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MappingMenuItemsWithModifier> MappingMenuItemsWithModifiers { get; set; }

    public virtual DbSet<MenuCategory> MenuCategories { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Modifier> Modifiers { get; set; }

    public virtual DbSet<ModifierGroup> ModifierGroups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderTaxMapping> OrderTaxMappings { get; set; }

    public virtual DbSet<OrderedItem> OrderedItems { get; set; }

    public virtual DbSet<OrderedItemModifierMapping> OrderedItemModifierMappings { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<TableOrderMapping> TableOrderMappings { get; set; }

    public virtual DbSet<TaxesAndFee> TaxesAndFees { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WaitingToken> WaitingTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("accounts_pkey");

            entity.ToTable("accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsFirstLogin)
                .HasDefaultValueSql("true")
                .HasColumnName("is_first_login");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.AccountCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.AccountModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("accounts_role_id_fkey");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("state_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id");

            entity.ToTable("countries");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.CustomerCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.CustomerModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feedback_pkey");

            entity.ToTable("feedbacks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ambience).HasColumnName("ambience");
            entity.Property(e => e.AvgRating).HasColumnName("avg_rating");
            entity.Property(e => e.Comments)
                .HasMaxLength(200)
                .HasColumnName("comments");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Food).HasColumnName("food");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Service).HasColumnName("service");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FeedbackCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.FeedbackModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.QuantityOfModifier).HasColumnName("quantity_of_modifier");
            entity.Property(e => e.RateOfModifier)
                .HasPrecision(18, 2)
                .HasColumnName("rate_of_modifier");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(18, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InvoiceCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InvoiceModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Modifier).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ModifierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");
        });

        modelBuilder.Entity<MappingMenuItemsWithModifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mapping_menu_items_with_modifiers_pkey");

            entity.ToTable("mapping_menu_items_with_modifiers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.MaxSelectionAllowed).HasColumnName("max_selection_allowed");
            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.MinSelectionRequired).HasColumnName("min_selection_required");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifierGroupId).HasColumnName("modifier_group_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MappingMenuItemsWithModifierCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.MappingMenuItemsWithModifiers)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_item_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MappingMenuItemsWithModifierModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.ModifierGroup).WithMany(p => p.MappingMenuItemsWithModifiers)
                .HasForeignKey(d => d.ModifierGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_group_id");
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_category_pkey");

            entity.ToTable("menu_categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuCategoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MenuCategoryModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_items_pkey");

            entity.ToTable("menu_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("true")
                .HasColumnName("is_available");
            entity.Property(e => e.IsDefaultTax)
                .HasDefaultValueSql("false")
                .HasColumnName("is_default_tax");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsFavourite)
                .HasDefaultValueSql("false")
                .HasColumnName("is_favourite");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(18, 2)
                .HasColumnName("rate");
            entity.Property(e => e.ShortCode)
                .HasMaxLength(50)
                .HasColumnName("short_code");
            entity.Property(e => e.TaxPercentage)
                .HasPrecision(18, 2)
                .HasColumnName("tax_percentage");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("true")
                .HasColumnName("type");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.Category).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("category_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.MenuItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Unit).WithMany(p => p.MenuItems)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unit_id");
        });

        modelBuilder.Entity<Modifier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modifier_pkey");

            entity.ToTable("modifiers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifierGroupId).HasColumnName("modifier_group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate)
                .HasPrecision(18, 2)
                .HasColumnName("rate");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModifierCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ModifierModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.ModifierGroup).WithMany(p => p.Modifiers)
                .HasForeignKey(d => d.ModifierGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_group_id");

            entity.HasOne(d => d.Unit).WithMany(p => p.Modifiers)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unit_id");
        });

        modelBuilder.Entity<ModifierGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("modifier_group_pkey");

            entity.ToTable("modifier_groups");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ModifierGroupCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.ModifierGroupModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Discount)
                .HasPrecision(18, 2)
                .HasColumnName("discount");
            entity.Property(e => e.IsSgstSelected)
                .HasDefaultValueSql("false")
                .HasColumnName("is_sgst_selected");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.OrderNo).HasColumnName("order_no");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.PaidAmount)
                .HasPrecision(18, 2)
                .HasColumnName("paid_amount");
            entity.Property(e => e.SubTotal)
                .HasPrecision(18, 2)
                .HasColumnName("sub_total");
            entity.Property(e => e.Tax)
                .HasPrecision(18, 2)
                .HasColumnName("tax");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(18, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OrderModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<OrderTaxMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_tax_mapping_pkey");

            entity.ToTable("order_tax_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.TaxValue)
                .HasPrecision(18, 2)
                .HasColumnName("tax_value");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderTaxMappingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OrderTaxMappingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderTaxMappings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");

            entity.HasOne(d => d.Tax).WithMany(p => p.OrderTaxMappings)
                .HasForeignKey(d => d.TaxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tax_id");
        });

        modelBuilder.Entity<OrderedItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordered_items_pkey");

            entity.ToTable("ordered_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Instruction).HasColumnName("instruction");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.MenuItemId).HasColumnName("menu_item_id");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.ReadyItemQuantity).HasColumnName("ready_item_quantity");
            entity.Property(e => e.Tax).HasColumnName("tax");
            entity.Property(e => e.TotelAmount).HasColumnName("totel_amount");
            entity.Property(e => e.TotelModifierAmount).HasColumnName("totel_modifier_amount");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderedItemCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.MenuItem).WithMany(p => p.OrderedItems)
                .HasForeignKey(d => d.MenuItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_item_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OrderedItemModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderedItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");
        });

        modelBuilder.Entity<OrderedItemModifierMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ordered_item_modifier_mapping_pkey");

            entity.ToTable("ordered_item_modifier_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifierId).HasColumnName("modifier_id");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.QuantityOfModifier).HasColumnName("quantity_of_modifier");
            entity.Property(e => e.RateOfModifier)
                .HasPrecision(18, 2)
                .HasColumnName("rate_of_modifier");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(18, 2)
                .HasColumnName("total_amount");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderedItemModifierMappingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.OrderedItemModifierMappingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Modifier).WithMany(p => p.OrderedItemModifierMappings)
                .HasForeignKey(d => d.ModifierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("modifier_id");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.OrderedItemModifierMappings)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_item_id");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PaymentCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PaymentModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permission_id");

            entity.ToTable("permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PermissionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.PermissionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_id");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permissions_pkey");

            entity.ToTable("role_permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CanDelete)
                .HasDefaultValueSql("false")
                .HasColumnName("can_delete");
            entity.Property(e => e.CanEdit)
                .HasDefaultValueSql("false")
                .HasColumnName("can_edit");
            entity.Property(e => e.CanView)
                .HasDefaultValueSql("false")
                .HasColumnName("can_view");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RolePermissionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.RolePermissionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permissions_permission_id_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permissions_role_id_fkey");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("section_pkey");

            entity.ToTable("sections");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.SectionCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.SectionModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("state_pkey");

            entity.ToTable("states");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("states_country_id_fkey");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tables_pkey");

            entity.ToTable("tables");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValueSql("true")
                .HasColumnName("is_available");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SectionId).HasColumnName("section_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TableCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TableModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Section).WithMany(p => p.Tables)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_id");
        });

        modelBuilder.Entity<TableOrderMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("table_order_mapping_pkey");

            entity.ToTable("table_order_mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.NoOfPeople).HasColumnName("no_of_people");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TableOrderMappingCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TableOrderMappingModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Order).WithMany(p => p.TableOrderMappings)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_id");

            entity.HasOne(d => d.Table).WithMany(p => p.TableOrderMappings)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("table_id");
        });

        modelBuilder.Entity<TaxesAndFee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("taxes_and_fees_pkey");

            entity.ToTable("taxes_and_fees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FlatAmount)
                .HasPrecision(18, 2)
                .HasColumnName("flat_amount");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("false")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDefault)
                .HasDefaultValueSql("false")
                .HasColumnName("is_default");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Percentage)
                .HasPrecision(18, 2)
                .HasColumnName("percentage");
            entity.Property(e => e.TaxValue)
                .HasPrecision(18, 2)
                .HasColumnName("tax_value");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("true")
                .HasColumnName("type");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TaxesAndFeeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.TaxesAndFeeModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("unitsp_key");

            entity.ToTable("units");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.ShortName)
                .HasMaxLength(50)
                .HasColumnName("short_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_id");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.ProfileImage)
                .HasColumnType("character varying")
                .HasColumnName("profile_image");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(50)
                .HasColumnName("zipcode");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("city_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("users_country_id_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("state_id");
        });

        modelBuilder.Entity<WaitingToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("waiting_token_pkey");

            entity.ToTable("waiting_tokens");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.IsAssigned)
                .HasDefaultValueSql("false")
                .HasColumnName("is_assigned");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedAt).HasColumnName("modified_at");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.NoOfPeople).HasColumnName("no_of_people");
            entity.Property(e => e.SectionId).HasColumnName("section_id");
            entity.Property(e => e.TableId).HasColumnName("table_id");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.WaitingTokenCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("created_by");

            entity.HasOne(d => d.Customer).WithMany(p => p.WaitingTokens)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_id");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.WaitingTokenModifiedByNavigations)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("modified_by");

            entity.HasOne(d => d.Section).WithMany(p => p.WaitingTokens)
                .HasForeignKey(d => d.SectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("section_id");

            entity.HasOne(d => d.Table).WithMany(p => p.WaitingTokens)
                .HasForeignKey(d => d.TableId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("table_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
