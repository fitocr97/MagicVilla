﻿using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models.ViewModels;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_Web.Controllers
{
    public class NumeroVillaController : Controller
    {
        private readonly INumeroVillaService _numeroVillaService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public NumeroVillaController(INumeroVillaService numeroVillaService, IMapper mapper, IVillaService villaService)
        {
            _mapper = mapper;
            _numeroVillaService = numeroVillaService;
            _villaService = villaService;
        }
        public async Task<IActionResult> Index()
        {
            List<NumeroVillaDto> numeroVillaList = new();

            var response = await _numeroVillaService.GetAll<APIResponse>();

            if (response != null && response.IsSuccessful)
            {
                numeroVillaList = JsonConvert.DeserializeObject<List<NumeroVillaDto>>(Convert.ToString(response.Result)); //
            }

            return View(numeroVillaList); //enviamos la lista a la vista
        }

        public async Task<IActionResult> CreateNumeroVilla()
        {
            NumeroVillaViewModel numeroVillaM = new();
            
            var response = await _villaService.GetAll<APIResponse>();

            if(response != null && response.IsSuccessful)
            {
                numeroVillaM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result))
                    .Select(v => new SelectListItem
                    {
                        Text = v.Nombre,
                        Value=v.Id.ToString()
                    });
            }

            return View(numeroVillaM); //retrun de todo el viewModel
        }



    }
}
