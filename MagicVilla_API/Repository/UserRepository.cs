using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<UserApp> userManager,
                                  IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;

        }

        //Verificar si el usuario ya existe metodo no async
        public bool IsUserUnico(string userName)
        {
            var user = _db.UsersApp.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower());
            if(user == null)            
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
         
            var user= await _db.UsersApp.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValido = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValido == false)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }

            //si existe generamos el JWT
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)
            };

            return loginResponseDto; //si esta autenticado se genra token y se devuelve el user
        }


        //primero lo tranformamos a async
        public async Task<UserDto> Register(RegistroRequestDto registroRequestDto)
        {
            UserApp user = new()
            {
                UserName = registroRequestDto.UserName,
                Email = registroRequestDto.UserName,
                NormalizedEmail = registroRequestDto.UserName.ToUpper(),
                Nombres = registroRequestDto.Name
            };

            try
            {
                var resultado = await _userManager.CreateAsync(user, registroRequestDto.Password); //propio de U manager
                if (resultado.Succeeded) //propio de U manager
                {
                    
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()) //si rol no existe lo crea
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("cliente"));
                    }


                    await _userManager.AddToRoleAsync(user, "admin");
                    var usuarioAp = _db.UsersApp.FirstOrDefault(u => u.UserName == registroRequestDto.UserName);
                    return _mapper.Map<UserDto>(usuarioAp);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return new UserDto();

            /* //YA NO SE VA USAR DBCONTEXT
            await _db.Users.AddAsync(user); //registar el user en la db
            await _db.SaveChangesAsync();
            user.Password = ""; //borrrar la password para que no se vea
            return user;*/
        }
    }
}
