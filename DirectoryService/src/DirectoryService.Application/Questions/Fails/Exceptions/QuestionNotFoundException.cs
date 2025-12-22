using DirectoryService.Application.Exceptions;
using Shared;

namespace DirectoryService.Application.Questions.Fails.Exceptions;

public class QuestionNotFoundException: NotFoundException
{
    public QuestionNotFoundException(Error[] errors) 
        : base(errors)
    {
    }
}