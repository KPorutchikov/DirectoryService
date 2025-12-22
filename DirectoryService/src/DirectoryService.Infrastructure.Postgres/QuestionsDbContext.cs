using DirectoryService.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace DirectoryService.Infrastructure.Postgres;

public class QuestionsDbContext: DbContext
{
    public DbSet<Question> Questions { get; set; }
    
}