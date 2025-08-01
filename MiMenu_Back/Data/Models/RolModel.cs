namespace MiMenu_Back.Data.Models
{
    public class RolModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public UserModel User { get; set; }
    }
}
