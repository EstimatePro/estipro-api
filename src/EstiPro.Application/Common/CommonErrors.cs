using FluentResults;

namespace EstiPro.Application.Common;

public static class CommonErrors
{
    private static IError EntityNotFoundError(string entityName, string searchByField) => new Error($"{entityName} with {searchByField} not found.");
    public static IError EntityNotFoundError<T>(string searchByField) => EntityNotFoundError(nameof(T), searchByField);
}