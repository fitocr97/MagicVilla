using Microsoft.AspNetCore.Identity;

namespace MagicVilla_API.Models
{
    public class UserApp : IdentityUser  //Agregar propiedades extra de la tabla que se va a crear
    {
        public string Nombres { get; set; }
    }
}
