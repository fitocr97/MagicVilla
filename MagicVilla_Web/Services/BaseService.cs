using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MagicVilla_Web.Services
{
    public class BaseService : IBaseService //implemetamos interface
    {
        //httpclient factory siver para todo tipo de conexiones ya registrado en .net core
        public IHttpClientFactory _httpClient { get; set; }
        public APIResponse responseModel { get; set; }

        public BaseService(IHttpClientFactory httpClient) //inyectamos httpclient
        {
            this.responseModel = new(); //inicializamos el responseModel
            _httpClient = httpClient;
        }

        

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client =_httpClient.CreateClient("MagicAPI"); //primero crear el cliente y darle un nombre al servicio
                HttpRequestMessage message = new HttpRequestMessage();  //luego ell llamado del mesanje por medio del httpsRequest
                                                                        //este necesita varios factores
                message.Headers.Add("Accept", "application/json"); //

                //se agrego para el pagination
                if (apiRequest.Parametros == null)
                {
                    message.RequestUri = new Uri(apiRequest.Url); //normal si viene vacio
                }
                else
                {
                    var builder = new UriBuilder(apiRequest.Url);
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["PageNumber"] = apiRequest.Parametros.PageNumber.ToString(); //armar la url
                    query["PageSize"] = apiRequest.Parametros.PageSize.ToString(); //armar la url
                    builder.Query = query.ToString();
                    string url = builder.ToString();     //   api/Villa/VillaPaginado/PageNumber=1&PageSize=4
                    message.RequestUri = new Uri(url);
                }


                if (apiRequest.Datos != null) //si es diferente de null se refiera post o put envian datos
                {
                        message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Datos),
                        Encoding.UTF8, "application/json");  //solo para estos casos se configura el content
                }

                switch (apiRequest.APITipo)
                {
                    case DS.APITipo.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case DS.APITipo.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case DS.APITipo.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case DS.APITipo.GET:
                        message.Method = HttpMethod.Get;
                        break;
                }

                //confiuguramos el response
                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token)) //verificar el token
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

                apiResponse = await client.SendAsync(message); //contiene todo
                var apiContent = await apiResponse.Content.ReadAsStringAsync(); //reciva el contenido de la respuesta

                try
                {
                    //Si da error llenamos el response
                    APIResponse response = JsonConvert.DeserializeObject<APIResponse>(apiContent); //api content es lo que devuelve el api
                    if (response != null && (apiResponse.StatusCode == HttpStatusCode.BadRequest
                                        || apiResponse.StatusCode == HttpStatusCode.NotFound))
                    {
                        response.statusCode = HttpStatusCode.BadRequest;
                        response.IsSuccessful = false;
                        var res = JsonConvert.SerializeObject(response);
                        var obj = JsonConvert.DeserializeObject<T>(res);
                        return obj;
                    }
                }
                catch (Exception ex)
                {
                    var errorResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return errorResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);//conversion del contenido

                return APIResponse;
            
            }
            catch (Exception ex) 
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string>{ Convert.ToString(ex.Message)},
                    IsSuccessful = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var responseEX = JsonConvert.DeserializeObject<T>(res);
                return responseEX;
            }
           
        }
    }
}
