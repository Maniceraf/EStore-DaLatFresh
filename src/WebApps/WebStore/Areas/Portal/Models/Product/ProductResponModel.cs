using WebStore.Entities;

namespace WebStore.Areas.Portal.Models.Product
{
    public class ProductResponModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PreviewImage { get; set; } = null!;
        public string OrignalPrice { get; set; } = null!;
        public double Discount { get; set; }
        public string LastPrice { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string Vendor { get; set; } = null!;
        public int ViewCounts { get; set; } = 0;
    }
}