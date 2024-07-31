1. Technologies:
- DotNet Core 8.0
- Entity Framework Core (ORM)
- Dapper

2.Update Database:
Scaffold-DbContext 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=projectDb;Trusted_Connection=True;' Microsoft.EntityFrameworkCore.SqlServer -Context ApplicationDbContext -ContextNamespace WebStore.Data -ContextDir ../WebStore/Data -OutputDir ../WebStore/Entities -Namespace WebStore.Entities -force -schema dbo