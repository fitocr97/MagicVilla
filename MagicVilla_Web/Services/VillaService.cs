using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;

namespace MagicVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        public readonly IHttpClientFactory _httpClient;
        private string _villaUrl;
        public VillaService(IHttpClientFactory httpClient, IConfiguration configuration) : base(httpClient) //lo solicita el padre el base
        {
            _httpClient = httpClient;
            _villaUrl = configuration.GetValue<string>("ServiceUrls:API_URL");
        }
        public Task<T> Create<T>(VillaCreateDto dto, string token)
        {
            //SendAsync metodo que esta en baseservices
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.POST,
                Datos = dto,
                Url = _villaUrl+"/api/Villa",
                Token = token
            });
        }

        public Task<T> Delete<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/Villa/" + id,
                Token = token
            });
        }

        public Task<T> GetAll<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/Villa",
                Token = token
            });
        }

        public Task<T> GetOne<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/Villa/" + id,
                Token = token
            });
        }

        public Task<T> Update<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/Villa/"+dto.Id,
                Token = token
            });
        }
    }
}
