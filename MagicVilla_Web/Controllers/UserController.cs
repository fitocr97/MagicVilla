using MagicVilla_Utility;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using MagicVilla_Web.Models;
using System.IdentityModel.Tokens.Jwt;

namespace MagicVilla_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto modelo)
        {

            var response = await _userService.Login<APIResponse>(modelo);
            if (response != null && response.IsSuccessful == true) //si user se conecta correctamente
            {
                //convertir el contenido que devuelve el api
                LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));

                var handler = new JwtSecurityTokenHandler(); //leer el token
                var jwt = handler.ReadJwtToken(loginResponse.Token); //devuelve el token

                //Claims tener guardado el username y rol 
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                //ya lo trae el token
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(c => c.Type == "unique_name").Value)); 
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(c => c.Type == "role").Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                

                // Session  accedemos al DS para no escribir el nombre
                //esa seccion se guarda con ese nombre
                HttpContext.Session.SetString(DS.SessionToken, loginResponse.Token);

    
                //retorna al index (metodo controlador)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                return View(modelo);
            }

        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistroRequestDto modelo)
        {
            var response = await _userService.Registrar<APIResponse>(modelo);
            if (response != null && response.IsSuccessful)
            {
                return RedirectToAction("login"); //si se registro con exito lo redirecciona al login
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            //limpia la seccion
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(DS.SessionToken, "");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

    }
}
