namespace WebStore.ViewModels
{
	public class ProductDetailViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string PreviewImage { get; set; } = null!;
		public string Price { get; set; } = null!;
		public string ShortDescription { get; set; } = null!;
		public string Category { get; set; } = null!;
		public string Description { get; set; } = null!;
		public int Rate { get; set; } = 5;
		public int RemainsCount { get; set; } = 10;
	}
}
