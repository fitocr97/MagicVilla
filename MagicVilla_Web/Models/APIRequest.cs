using static MagicVilla_Utility.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;
        public string Url { get; set; }
        public object Datos {  get; set; } //manejo de los datos
        public string Token { get; set; }
    }
}
