using MagicVilla_Web.Models;

namespace MagicVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel { get; set; } 

        Task<T> SendAsync<T>(APIRequest apiRequest); //servicio generecio utilizado para hacer todo tipo de llamadas y recive la solicitud
    }
}
