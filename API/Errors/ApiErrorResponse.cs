using System.Diagnostics;

namespace API.Errors;

public class ApiErrorResponse(int statusCode, string message, string? details)
{

    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public string? Details { get; set; } = details;
    
    private string? GetDefaultMessageForStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "A Bad Request, you have made",
            401 => "Authorized, you are not",
            404 => "Resource found, it was not",
            500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change.",
            _ => null
        };
    }
}