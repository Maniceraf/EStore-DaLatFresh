namespace WebStore.Areas.Portal.Models.CategoryModels
{
    public class CreateCategoryRequestModel
    {
        public string Name { get; set; } = null!;

        public string Alias { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
