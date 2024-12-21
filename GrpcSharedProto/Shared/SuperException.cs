namespace GrpcSharedProto.Shared
{
    public class SuperException : Exception
    {
        public SuperException(string message, Exception? exception) : base(message, exception)
        {
            string error = exception.Message;
            Console.WriteLine(error += exception != null ? exception.StackTrace : string.Empty);
        }
    }
}