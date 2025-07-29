namespace ErrorHandling.Error;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string message)
        : base(message)
    {
    }
}