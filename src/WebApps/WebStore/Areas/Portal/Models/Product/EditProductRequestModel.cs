﻿namespace WebStore.Areas.Portal.Models.Product
{
    public class EditProductRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Alias { get; set; } = null!;

        public string UnitDescription { get; set; } = null!;

        public double Price { get; set; }

        public IFormFile? ImageFile { get; set; }

        public double Discount { get; set; }

        public string Description { get; set; } = null!;

        public int ProductTypeId { get; set; }

        public int VendorId { get; set; }
    }
}
