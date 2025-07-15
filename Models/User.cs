namespace Canguro.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "Usuario";
        public bool Activo { get; set; } = true;
    }
}
