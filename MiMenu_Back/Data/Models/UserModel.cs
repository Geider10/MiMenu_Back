namespace MiMenu_Back.Data.Models
{
    public class UserModel
    {
        public Guid Id { get; protected set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Role { get; protected set; }
        public DateOnly? BirthDate { get; set; }
        public UserModel()
        {
            Id = Guid.NewGuid();
            Role = "client";
        }

    }
}
