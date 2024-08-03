namespace WebStore.Areas.Portal.Models.CategoryModels
{
    public class EditCategoryRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Alias { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
