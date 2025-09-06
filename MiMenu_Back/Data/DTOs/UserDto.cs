namespace MiMenu_Back.Data.DTOs
{
    public class SignupDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? BirthDate { get; set; }
    }
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserGetDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? BirthDate { get; set; }
    }
    public class UserUpdateDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string BirthDate { get; set; }
    }
}
