using DirectoryService.Application.Exceptions;
using Shared;

namespace DirectoryService.Application.Questions.Fails.Exceptions;

public class QuestionTooManyQuestionsException: BadRequestException 
{
    public QuestionTooManyQuestionsException()
        : base([Errors.Questions.ToManyQuestions()]) 
    {
        
    }
}