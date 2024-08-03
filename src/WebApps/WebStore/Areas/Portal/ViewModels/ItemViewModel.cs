namespace WebStore.Areas.Portal.ViewModels
{
    public class ItemViewModel
    {
        public string Action { get; set; } = null!;
        public string Controller { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = false;
        public List<string> Childrens { get; set; } = [];
    }
}
