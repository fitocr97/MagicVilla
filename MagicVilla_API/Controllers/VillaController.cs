﻿using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        //endpoint
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas() 
        {
            return VillaStore.VillaList;
        }

        [HttpGet("id")] //"id:int"
        public VillaDto GetVilla(int id) 
        {
            return VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        }
    }
}
