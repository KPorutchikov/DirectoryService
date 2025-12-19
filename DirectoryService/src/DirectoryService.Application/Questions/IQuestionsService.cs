using DirectoryService.Contracts.Questions;

namespace DirectoryService.Application.Questions;

public interface IQuestionsService
{
    Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken ct);
}