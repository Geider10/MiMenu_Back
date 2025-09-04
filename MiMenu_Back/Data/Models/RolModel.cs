namespace MiMenu_Back.Data.Models
{
    public class RolModel
    {
        public Guid Id { get; set; }
        public TypeRolEnum Type { get; set; }

        public ICollection<UserModel> Users { get; set; }
    }
    public enum TypeRolEnum
    {
        Client = 0,
        Admin = 1,
        Local = 2
    }
}
