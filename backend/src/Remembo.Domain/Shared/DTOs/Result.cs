using System.Net;

namespace Remembo.Domain.Shared.DTOs;
public class Result<T> {
    public T? Data { get; private set; }
    public HttpStatusCode Status { get; private set; }
    public List<string> Errors { get; private set; } = [];
    public string? ExceptionMessage { get; private set; }
    public bool HasErrors => Errors.Count != 0;
    public bool Success => !HasErrors;
    public string? SuccessMessage { get; private set; }

    public Result(T data, List<string> errors, HttpStatusCode status, string? successMessage = null) {
        Data = data;
        Errors = errors;
        Status = status;
        SuccessMessage = successMessage;
    }

    public Result(T data, HttpStatusCode status, string? successMessage = null) {
        Data = data;
        Status = status;
        SuccessMessage = successMessage;
    }

    public Result(HttpStatusCode status, string? successMessage = null) {
        Status = status;
        SuccessMessage = successMessage;
    }

    public Result(List<string> errors, HttpStatusCode status) {
        Errors = errors;
        Status = status;
    }

    public Result(string error, HttpStatusCode status) {
        Errors.Add(error);
        Status = status;
    }

    public Result(string error, string exceptionMessage, HttpStatusCode status) {
        Errors.Add(error);
        ExceptionMessage = exceptionMessage;
        Status = status;
    }
}

