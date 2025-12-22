using CSharpFunctionalExtensions;
using DirectoryService.Application.Extensions;
using DirectoryService.Application.Questions.Fails;
using DirectoryService.Application.Questions.Fails.Exceptions;
using DirectoryService.Contracts.Questions;
using DirectoryService.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;
using DirectoryService.Application.Extensions;

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

    public async Task<Result<Guid, Failure>> Create(CreateQuestionDto questionDto, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(questionDto, ct);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrors();
        }
        
        int openUserQuestionsCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, ct);
        if (openUserQuestionsCount > 3)
        {
            return Errors.Questions.ToManyQuestions().ToFailure();
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

// public class Result
// {
//     public bool IsSuccess { get; }
//     
//     public bool IsFailure => !IsSuccess;
//     
//     public Error Error { get; }
//
//     protected Result()
//     {
//         IsSuccess = true;
//         Error = Error.None;
//     }
//
//     protected Result(Error error)
//     {
//         IsSuccess = false;
//         Error = error;
//     }
//
//     public static Result Success() => new();
//     
//     public static Result Failure(Error error) => new(error);
//     
//     public static implicit operator Result(Error error) => Failure(error);
// }
//
// public sealed class Result<TValue> : Result
// {
//     private readonly TValue _value = default!;
//     
//     private Result(TValue value) => _value = value;
//
//     private Result(Error error)
//         : base(error)
//     {
//     }
//     
//     public static Result<TValue> Success(TValue value) => new(value);
//     
//     public new static Result<TValue> Failure(Error error) => new(error);
//     
//     public static implicit operator Result<TValue>(Error error) => Failure(error);
//     
//     public static implicit operator Result<TValue>(TValue value) => Success(value);
//     
//     public static implicit operator TValue(Result<TValue> value) => value._value;
//
//     public TValue Value => IsSuccess ? _value : throw new ApplicationException("Result is not success");
// }