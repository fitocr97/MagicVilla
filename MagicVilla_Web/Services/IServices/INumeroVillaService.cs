using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface INumeroVillaService
    {
        Task<T> GetAll<T>();
        Task<T> GetOne<T>(int id);

        Task<T> Create<T>(NumeroVillaCreateDto dto);
        Task<T> Update<T>(NumeroVillaUpdateDto dto);
        Task<T> Delete<T>(int id);
    }
}
