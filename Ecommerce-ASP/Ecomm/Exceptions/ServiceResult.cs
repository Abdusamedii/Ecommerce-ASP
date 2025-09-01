namespace Ecomm.Exceptions;

public class ServiceResult<T>
{
    public bool success { get; set; }
    public T? data { get; set; }
    public string errorMessage { get; set; }
    
    
}
