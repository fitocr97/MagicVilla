namespace MagicVilla_API.Models.Dto
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token {  get; set; } //token autenticacion
        public string Rol {  get; set; }
        
    }
}
