using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebStore.Data;
using WebStore.Implements;
using WebStore.Interfaces;
using WebStore.Interfaces.Repositories;

namespace WebStore
{
    public static class ProgramExtensions
    {
        public static void AddBusinessService(this WebApplicationBuilder builder)
        {

        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("projectDBConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(c =>
            {
                c.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.CommandTimeout(120);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
                c.UseLazyLoadingProxies();
            });

            builder.Services.AddScoped<DbContext, ApplicationDbContext>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            var allRepositoryInterfaces = Assembly.GetAssembly(typeof(IGenericRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();
            var allRepositoryImplements = Assembly.GetAssembly(typeof(GenericRepository<>))
                ?.GetTypes().Where(t => t.Name.EndsWith("Repository")).ToList();

            if (allRepositoryInterfaces != null)
            {
                foreach (var repositoryType in allRepositoryInterfaces.Where(t => t.IsInterface))
                {
                    if (allRepositoryImplements != null)
                    {
                        var implement = allRepositoryImplements.Find(c => c.IsClass && repositoryType.Name.Substring(1) == c.Name);
                        if (implement != null)
                        {
                            builder.Services.AddScoped(repositoryType, implement);
                        }
                    }
                }
            }
        }
    }
}
