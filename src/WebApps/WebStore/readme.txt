1. Technologies:
- DotNet Core 8.0
- Entity Framework Core (ORM)
- Dapper

2. Services

- Session: Session thường được sử dụng để lưu trữ thông tin người dùng giữa các yêu cầu trong các ứng dụng web truyền thống, chẳng hạn như ASP.NET MVC. Ví dụ:

Giỏ hàng: Lưu trữ các mục trong giỏ hàng của người dùng.
Thông tin người dùng: Lưu trữ thông tin đăng nhập hoặc các thuộc tính khác của người dùng hiện tại.
Cài đặt tùy chỉnh: Lưu trữ các cài đặt giao diện hoặc tùy chọn người dùng.

3.Update Database:
Scaffold-DbContext 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=projectDb;Trusted_Connection=True;' Microsoft.EntityFrameworkCore.SqlServer -Context ApplicationDbContext -ContextNamespace WebStore.Data -ContextDir ../WebStore/Data -OutputDir ../WebStore/Entities -Namespace WebStore.Entities -force -schema dbo