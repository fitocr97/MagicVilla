using AutoMapper;
using Azure;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Net;

namespace MagicVilla_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class NumeroVillaController : ControllerBase
    {   
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly INumeroVillaRepository _numeroRepo;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, INumeroVillaRepository numeroRepo, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _numeroRepo = numeroRepo;  //incializa
            _villaRepo = villaRepo;
            _mapper = mapper;
            _apiResponse = new APIResponse();
        }

     
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "valor1", "valor2" };
        }
    }
}
