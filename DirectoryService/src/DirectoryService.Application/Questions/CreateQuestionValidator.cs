using DirectoryService.Contracts.Questions;
using FluentValidation;

namespace DirectoryService.Application.Questions;

public class CreateQuestionValidator: AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(q => q.Title).NotEmpty().MaximumLength(1000).WithMessage("Заголовок не валидный.");
        RuleFor(q => q.Text).NotEmpty().MaximumLength(5000).WithMessage("Текст не валидный.");
        RuleFor(q => q.UserId).NotEmpty();
    }
}