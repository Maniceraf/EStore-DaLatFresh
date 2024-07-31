using System;
using System.Collections.Generic;

namespace WebStore.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Alias { get; set; }

    public string? UnitDescription { get; set; }

    public double? Price { get; set; }

    public string? PreviewImage { get; set; }

    public DateTime ProductionDateOnLocal { get; set; }

    public DateTime ProductionDateOnUtc { get; set; }

    public double Discount { get; set; }

    public int ViewCounts { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedOnLocal { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? UpdatedOnLocal { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public int CategoryId { get; set; }

    public int VendorId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
