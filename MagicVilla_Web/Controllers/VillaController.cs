using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<VillaDto> villaList = new();
            var response = await _villaService.GetAll<APIResponse>(HttpContext.Session.GetString(DS.SessionToken)); //encargada de traer toda la lista API response retorna todas la propiedades

            if (response != null && response.IsSuccessful) 
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }

            return View(villaList);
        }

        //Create

        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDto modelo)
        {
            if (ModelState.IsValid)  //si esta valido el modelo campos requeridos
            {
                var response = await _villaService.Create<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if (response != null && response.IsSuccessful) 
                {
                    TempData["exitoso"] = "Villa Creada Exitosamente";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(modelo);
        }


        public async Task<IActionResult> UpdateVilla(int villaId)
        {

            var response = await _villaService.GetOne<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));

            if(response != null && response.IsSuccessful) 
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(_mapper.Map<VillaUpdateDto>(model)); //convierte el modelo a VillaUpdateDto porque estaba en VillaDto
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto modelo)
        {

            if (ModelState.IsValid) 
            {
                var response = await _villaService.Update<APIResponse>(modelo, HttpContext.Session.GetString(DS.SessionToken));

                if (response != null && response.IsSuccessful)
                {
                    TempData["exitoso"] = "Villa Acutalizada Exitosamente";
                    return RedirectToAction(nameof(Index)); 
                }

            }

            return View(modelo); //si no es valido
        }


        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetOne<APIResponse>(villaId, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsSuccessful)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDto modelo)
        {

            var response = await _villaService.Delete<APIResponse>(modelo.Id, HttpContext.Session.GetString(DS.SessionToken));

            if (response != null && response.IsSuccessful)
            {
                TempData["exitoso"] = "Villa Eliminada Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Ocurrio un Error al Eliminar";
            return View(modelo);
        }
    }
}
