using MagicVilla_Web.Models.Dto;
using NuGet.Common;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAll<T>(string Token );
        Task<T> GetOne<T>(int id, string Token);

        Task<T> Create<T>(VillaCreateDto dto, string Token);
        Task<T> Update<T>(VillaUpdateDto dto, string Token);
        Task<T> Delete<T>(int id, string Token);
    }
}
