namespace Ecomm.Exceptions;

public class BadRequestResponse
{
    public BadRequestResponse(bool success = false, string message = null)
    {
        success = success;
        errorMessage = message;
    }
    public bool success { get; set; }
    public string errorMessage { get; set; }

    public BadRequestResponse fail(string message)
    {
        return new BadRequestResponse(false, message);
    }

    public BadRequestResponse Ok(string message)
    {
        return new BadRequestResponse(true, message);
    }
}