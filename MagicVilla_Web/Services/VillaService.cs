﻿using MagicVilla_Utility;
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
                Url = _villaUrl+ "/api/v1/Villa",
                Token = token
            });
        }

        public Task<T> Delete<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.DELETE,
                Url = _villaUrl + "/api/v1/Villa/" + id,
                Token = token
            });
        }

        public Task<T> GetAll<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/v1/Villa",
                Token = token
            });
        }

        public Task<T> GetAllPaginado<T>(string token, int pageNumber = 1, int pageSize = 4)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/v1/Villa/VillasPaginado",
                Token = token,
                Parametros = new Parametros() { PageNumber = pageNumber, PageSize = pageSize }
            });
        }

        public Task<T> GetOne<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.GET,
                Url = _villaUrl + "/api/v1/Villa/" + id,
                Token = token
            });
        }

        public Task<T> Update<T>(VillaUpdateDto dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                APITipo = DS.APITipo.PUT,
                Datos = dto,
                Url = _villaUrl + "/api/v1/Villa/" + dto.Id,
                Token = token
            });
        }
    }
}
