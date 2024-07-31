namespace WebStore.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string PreviewImage { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public double Price { get; set; }
        public int Count { get; set; }
        public double Total => Price * Count;
    }
}
