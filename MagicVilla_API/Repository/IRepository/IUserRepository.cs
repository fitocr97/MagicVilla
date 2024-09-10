using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IUserRepository
    {
        //ver si el user es unico
        bool IsUserUnico(string userName);
        //metodo login
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        //metodo retorna el modelo tipo user el user que se registra
        Task<UserDto> Register(RegistroRequestDto registroRequestDto);
    }
}
