namespace Bazario.Users.Application.Exceptions
{
    public sealed class InternalSystemException : Exception
    {
        public InternalSystemException() 
            : base("Encountered an error due to internal errors.")
        { }

        public InternalSystemException(string details)
            : base($"Encountered an error due to internal errors. Details: {details}")
        { }
    }
}
