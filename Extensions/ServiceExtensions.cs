using PersonalDataDirectory.Application.Interfaces;
using PersonalDataDirectory.Application.Services;
using PersonalDataDirectory.Infrastructure.Repositories;
using PersonalDataDirectory.Infrastructure.UnitOfWork;

namespace PersonalDataDirectory.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonSearchService, PersonSearchService>();
        services.AddScoped<IPersonReportService, PersonReportService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<IRelationshipService, RelationshipService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
    }
}