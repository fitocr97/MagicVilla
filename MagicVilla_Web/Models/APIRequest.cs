using static MagicVilla_Utility.DS;

namespace MagicVilla_Web.Models
{
    public class APIRequest
    {
        public APITipo APITipo { get; set; } = APITipo.GET;
        public string Url { get; set; }
        public object Datos {  get; set; } //manejo de los datos
        public string Token { get; set; }

        //nuevo abritbuto
        public Parametros Parametros { get; set; }

    }

    //clase nueva parametro
    public class Parametros
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
