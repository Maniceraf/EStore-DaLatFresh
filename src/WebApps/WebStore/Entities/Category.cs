using System;
using System.Collections.Generic;

namespace WebStore.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Alias { get; set; }

    public string? Description { get; set; }

    public string? PreviewImage { get; set; }

    public DateTime CreatedOnLocal { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? UpdatedOnLocal { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
