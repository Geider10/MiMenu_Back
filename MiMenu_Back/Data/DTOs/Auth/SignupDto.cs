namespace MiMenu_Back.Data.DTOs.Auth
{
    public class SignupDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? BirthDate { get; set; }
    }
}
