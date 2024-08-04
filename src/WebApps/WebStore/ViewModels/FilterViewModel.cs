namespace WebStore.ViewModels
{
	public class FilterViewModel
	{
		public List<CategoryFilterViewModel> Categories { get; set; } = [];
		public List<VendorFilterViewModel> Vendors { get; set; } = [];
	}

	public class VendorFilterViewModel
	{
		public int Id {  get; set; }
		public string Name { get; set; } = null!;
	}

	public class CategoryFilterViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
	}
}
