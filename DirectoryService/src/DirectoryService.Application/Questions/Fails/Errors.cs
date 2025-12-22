using Shared;

namespace DirectoryService.Application.Questions.Fails;

public partial class Errors
{
    public static class Questions
    {
        public static Error ToManyQuestions() =>
            Error.Failure("question.too.many", "Пользователь не может открыть более 3 открытых вопросов.");
    }
    
}