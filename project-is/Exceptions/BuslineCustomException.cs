namespace project_is.Exceptions
{
    public class BuslineCustomException : Exception
    {
        public BuslineCustomException(): base () {}
        public BuslineCustomException(string? message): base (message) { }
    }
}
