namespace MiMenu_Back.Utils
{
    public class MainResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }

        public MainResponse(bool ok, string message)
        {
            Ok = ok;
            Message = message;
        }
    }
}
