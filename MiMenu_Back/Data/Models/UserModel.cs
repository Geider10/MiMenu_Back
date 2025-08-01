using Microsoft.AspNetCore.Authentication;

namespace MiMenu_Back.Data.Models
{
    public class UserModel
    {
        public Guid Id { get; protected set; }
        public Guid IdRol { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateOnly? BirthDate { get; set; }

        public RolModel Rol { get; set; }
        public ICollection<CartItem> Orders { get; set; }
        public UserModel()
        {
            Id = Guid.NewGuid();
            IdRol = Guid.Parse("00000000-0000-0000-0000-000000000001");
        }

    }
}
