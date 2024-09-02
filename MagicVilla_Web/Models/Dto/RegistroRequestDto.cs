namespace MagicVilla_Web.Models.Dto
{
    public class RegistroRequestDto //solicitud para crear un nuevo usuario
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
