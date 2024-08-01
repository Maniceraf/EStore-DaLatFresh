namespace WebStore.Areas.Portal.ViewModels
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? UnitDescription { get; set; }

		public double? Price { get; set; }

		public string? PreviewImage { get; set; }

		public double Discount { get; set; }

		public int ViewCounts { get; set; }

		public string? Description { get; set; }

		public int CategoryId { get; set; }

		public int VendorId { get; set; }

		public string Category { get; set; } = null!;

		public string Vendor { get; set; } = null!;
	}
}
