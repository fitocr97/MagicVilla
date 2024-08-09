using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;

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
                message.RequestUri = new Uri(apiRequest.Url); //URL por la cual nos vamos a conectar

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
                apiResponse = await client.SendAsync(message); //contiene todo
                var apiContent = await apiResponse.Content.ReadAsStringAsync(); //reciva el contenido de la respuesta
                Console.WriteLine(apiContent);
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);//conversion del contenido
                Console.WriteLine(APIResponse);

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
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
           
        }
    }
}
