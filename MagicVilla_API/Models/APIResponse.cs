using System.Net;

namespace MagicVilla_API.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>(); //inicializamos la lista para que no de futuros errores
        }
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public int TotalPaginas { get; set; }
    }
}
