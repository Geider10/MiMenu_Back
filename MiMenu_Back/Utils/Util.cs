using System.Text.RegularExpressions;

namespace MiMenu_Back.Utils
{
    public class Util
    {
        public string HashText(string text)
        {
            return BCrypt.Net.BCrypt.HashPassword(text);
        }
        public bool VerifyHashText(string text, string hashText)
        {
            return BCrypt.Net.BCrypt.Verify(text, hashText);
        }
        public DateOnly? FormatToDateOnly(string date)
        {
            var dateFormat = new Regex(@"^(?:3[01]|[12][0-9]|0?[1-9])([\-/.])(0?[1-9]|1[0-2])\1\d{4}$");
            if (!dateFormat.IsMatch(date)) return null;
            return DateOnly.Parse(date);
        }
    }
}
