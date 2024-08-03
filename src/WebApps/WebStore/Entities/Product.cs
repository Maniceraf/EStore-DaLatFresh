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

    public double Discount { get; set; }

    public int ViewCounts { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public int ProductTypeId { get; set; }

    public int VendorId { get; set; }

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual Vendor Vendor { get; set; } = null!;
}
