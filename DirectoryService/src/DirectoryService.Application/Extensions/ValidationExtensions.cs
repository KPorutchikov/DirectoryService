using FluentValidation.Results;
using Shared;

namespace DirectoryService.Application.Extensions;

public static class ValidationExtensions
{
    public static Failure ToErrors(this ValidationResult result) =>
        result.Errors.Select(e => Error.Validation(e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
}