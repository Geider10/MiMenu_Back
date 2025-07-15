namespace MiMenu_Back.Utils
{
    public class MainException : Exception
    {
        public int StatusCode { get; }
        public MainException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
