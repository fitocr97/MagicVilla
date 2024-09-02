namespace MagicVilla_Utility
{
    //Definiciones estaticas esta clase manejará todas las definiciones estaticas
    public static class DS 
    {

        public enum APITipo //maneja diferentes tipos de valores
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public static string SessionToken = "JWToken";
    }
}
