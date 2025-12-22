using DirectoryService.Application.Questions;
using DirectoryService.Infrastructure.Postgres.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DirectoryService.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<QuestionsDbContext>();
        services.AddScoped<IQuestionsRepository, QuestionsEfCoreRepository>();
        return services;
    }
}