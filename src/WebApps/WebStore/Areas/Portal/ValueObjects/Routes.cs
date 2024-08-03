using WebStore.Areas.Portal.ViewModels;

namespace WebStore.Areas.Portal.ValueObjects
{
    public static class Routes
    {
        public static List<MenuViewModel> GetAll(string menuItem)
        {
            return
            [
                new()
                {
                    Action = "Index",
                    Controller = "Dashboard",
                    Name = "Dashboard",
                    Icon = "fas fa-tachometer-alt",
                    IsActive = menuItem == "Dashboard",
                    Childrens = []
                },
                new() {
                    Action = "Index",
                    Controller = "Products",
                    Name = "Products",
                    Icon = "fas fas fa-th",
                    IsActive = menuItem == "Products",
                    Childrens = []
                },
                new() {
                    Action = "Index",
                    Controller = "Categories",
                    Name = "Categories",
                    Icon = "fas fas fa-copy",
                    IsActive = menuItem == "Categories",
                    Childrens = []
                },
            ];
        }
    }
}
