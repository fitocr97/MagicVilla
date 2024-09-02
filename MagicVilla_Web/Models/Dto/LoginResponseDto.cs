namespace MagicVilla_Web.Models.Dto
{
    public class LoginResponseDto //devuelve todo el usuario y token
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
