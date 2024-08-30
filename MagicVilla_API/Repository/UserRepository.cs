using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        //Verificar si el usuario ya existe metodo no async
        public bool IsUserUnico(string userName)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            if(user == null)            
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u=> u.UserName.ToLower()==loginRequestDto.UserName.ToLower() &&
            u.Password == loginRequestDto.Password);

            if (user == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }

            //si existe generamos el JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol.ToString())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };

            return loginResponseDto; //si esta autenticado se genra token y se devuelve el user
        }


        //primero lo tranformamos a async
        public async Task<User> Register(RegistroRequestDto registroRequestDto)
        {
            User user = new()
            {
                UserName = registroRequestDto.UserName,
                Password = registroRequestDto.Password,
                Name = registroRequestDto.Name,
                Rol = registroRequestDto.Rol
            };

            await _db.Users.AddAsync(user); //registar el user en la db
            await _db.SaveChangesAsync();
            user.Password = ""; //borrrar la password para que no se vea
            return user;
        }
    }
}
