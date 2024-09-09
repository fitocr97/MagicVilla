using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, IVillaService villaService)
        {
            _logger = logger;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1) //pasamos que inicie en la pagina 1
        {
            List<VillaDto> villaList = new();
            VillaPaginadoViewModel villaVM = new VillaPaginadoViewModel(); //declaro VM

            if (pageNumber < 1) pageNumber = 1; //pagenumber siempre sea 1

            var response = await _villaService.GetAllPaginado<APIResponse>(HttpContext.Session.GetString(DS.SessionToken), pageNumber, 4); //4 tamaño pagina

            if (response != null && response.IsSuccessful)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));

                villaVM = new VillaPaginadoViewModel()  //llenar el VM
                {
                    VillaList = villaList,
                    PageNumber = pageNumber,
                    TotalPaginas = JsonConvert.DeserializeObject<int>(Convert.ToString(response.TotalPaginas))
                };

                if (pageNumber > 1) villaVM.Previo = ""; //no se desabilite
                if (villaVM.TotalPaginas <= pageNumber) villaVM.Siguiente = "disabled";

            }

            return View(villaVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
