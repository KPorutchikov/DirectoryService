using DirectoryService.Contracts.Questions;
using DirectoryService.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
    private readonly IValidator<CreateQuestionDto> _validator;
    private readonly ILogger<QuestionsService> _logger;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        IValidator<CreateQuestionDto> validator,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken ct)
    {
        // Валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, ct);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        // Валидация бизнес логики
        int openUserQuestionsCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, ct);
        if (openUserQuestionsCount > 3)
        {
            throw new Exception("Пользователь не может открыть более 3 открытых вопросов.");
        }

        var questionId = Guid.NewGuid();
        var question = new Question(
            questionId, 
            questionDto.Title, 
            questionDto.Text, 
            questionDto.UserId, 
            null,
            questionDto.TagIds);

        await _questionsRepository.AddAsync(question, ct);

        // Логирование о результатах операции
        _logger.LogInformation("Created question with id: {questionId}", questionId);
        
        return questionId;
    }

    // public async Task Update(Guid questionId, UpdateQuestionDto request, CancellationToken ct)
    // {
    // }
    //
    // public async Task Delete(Guid questionId, CancellationToken ct)
    // {
    // }
    //
    // public async Task SelectSolution(Guid questionId, Guid answerId, CancellationToken ct)
    // {
    // }
    //
    // public async Task AddAnswer(Guid questionId, AddAnswerDto request, CancellationToken ct)
    // {
    // }
}