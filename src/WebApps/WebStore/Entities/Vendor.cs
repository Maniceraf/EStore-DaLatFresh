﻿using System;
using System.Collections.Generic;

namespace WebStore.Entities;

public partial class Vendor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? UpdatedOnUtc { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
