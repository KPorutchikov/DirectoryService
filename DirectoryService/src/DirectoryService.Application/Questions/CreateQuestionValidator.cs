using DirectoryService.Contracts.Questions;
using FluentValidation;

namespace DirectoryService.Application.Questions;

public class CreateQuestionValidator: AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty().WithMessage("Заголовок не должен быть пустым.")
            .MaximumLength(1000).WithMessage("Заголовок очень длиный.");
        
        RuleFor(q => q.Text)
            .NotEmpty().WithMessage("Текст не должен быть пустым.")
            .MaximumLength(5000).WithMessage("Текст очень длинный.");
        
        RuleFor(q => q.UserId).NotEmpty();
    }
}