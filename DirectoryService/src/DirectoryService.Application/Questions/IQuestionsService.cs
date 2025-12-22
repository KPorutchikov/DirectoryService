using CSharpFunctionalExtensions;
using DirectoryService.Contracts.Questions;
using Shared;

namespace DirectoryService.Application.Questions;

public interface IQuestionsService
{
    Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken ct);
}