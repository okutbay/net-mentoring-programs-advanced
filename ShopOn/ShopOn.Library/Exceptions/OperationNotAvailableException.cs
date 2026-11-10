namespace ShopOn.Library.Exceptions;

public class OperationNotAvailableException : Exception
{
    public OperationNotAvailableException()
    {
    }

    public OperationNotAvailableException(string message)
        : base(message)
    {
    }

    public OperationNotAvailableException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
