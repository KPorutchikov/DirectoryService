using FluentValidation.Results;
using Shared;


namespace DirectoryService.Application.Questions.Fails.Exceptions;


// public static class ValidationExceptions
// {
//     public static Failure ToErrors(this ValidationResult result) =>
//         result.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
// }