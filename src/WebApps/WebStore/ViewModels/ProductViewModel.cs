namespace WebStore.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PreviewImage { get; set; } = null!;
        public string Price { get; set; } = null!;
		public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
    }
}
