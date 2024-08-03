namespace WebStore.Areas.Portal.Models.ProductTypeModels
{
    public class EditProductTypeRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Alias { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
