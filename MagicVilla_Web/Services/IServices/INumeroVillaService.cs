using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> GetAll<T>(string token);
        Task<T> GetOne<T>(int id, string token);

        Task<T> Create<T>(NumeroVillaCreateDto dto, string token);
        Task<T> Update<T>(NumeroVillaUpdateDto dto, string token);
        Task<T> Delete<T>(int id, string token);
    }
}
