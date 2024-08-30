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

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        //getall
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetNumeroVillas() //RETORNA TIPO APIRESPONSE
        {
            try 
            {
                IEnumerable<NumeroVilla> numeroVillaList = await _numeroRepo.GetAll(incluirPropiedades:"Villa");

                _apiResponse.Result = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList); //agrega datos al response
                _apiResponse.statusCode = HttpStatusCode.OK; //using System.Net;
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }
            
            return _apiResponse; 
        }

        //getOne
        [HttpGet("{id:int}", Name = "GetNumeroVilla")] //"id:int"
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<APIResponse>> GetNumeroVilla(int id)
        {
            try 
            {
                if (id == 0)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                var numeroVilla = await _numeroRepo.GetOne(v => v.VillaNo == id, incluirPropiedades: "Villa"); //le enviamos el filtro por id en este caso VillaNo era el id

                if (numeroVilla == null)
                {
                    _apiResponse.statusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccessful = false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Result = _mapper.Map<NumeroVillaDto>(numeroVilla); //agrega datos al response
                _apiResponse.statusCode = HttpStatusCode.OK;

                return Ok(_apiResponse); //200 todo ok
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }

            return NotFound(_apiResponse);
        }

        //create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumeroVilla([FromBody] NumeroVillaCreateDto createDto)  //from body indica que vamos a recibir datos VillaDto tipo de dato que vamos a recibir y le ponemos el nombre
        {

            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //validar que no se repitan el mismo numero de villa 
                if (await _numeroRepo.GetOne(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El numero ya existe"); //Nombre del error, mensaje a mostrar
                    return BadRequest(ModelState);
                }

                //validar si existe o no el id Villa (padre)
                if (await _villaRepo.GetOne( v=>v.Id == createDto.VillaId ) == null)
                {
                    ModelState.AddModelError("Villa ID foreanea", "No existe la clave foranea id de la villa");
                    return BadRequest(ModelState);
                }


                if (createDto == null)
                {
                    return BadRequest(createDto); //no nos estan enviado datos
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;

                await _numeroRepo.Create(modelo); //INSERT
                _apiResponse.Result = modelo;
                _apiResponse.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _apiResponse); //se pasa el response
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }

            return _apiResponse;
        }

        //Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteNumeroVilla(int id) //aqui no se puede poner APIResponse, interface no puede llevar un tipo
        {

            try 
            {
                if (id == 0)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                //ver si el id encuentra algo en la lista
                var numeroVilla = await _numeroRepo.GetOne(v => v.VillaNo == id);

                if (numeroVilla == null)
                {
                    _apiResponse.statusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                await _numeroRepo.Delete(numeroVilla);
                _apiResponse.statusCode = HttpStatusCode.NoContent;

                return Ok(_apiResponse); //204 al nocontent no se le puede pasar parametros por eso se cambio a OK
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }

            return BadRequest(_apiResponse); //por no poder devolver APIResponse se devuelve el action result
        }

        //put
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto) //recibe todo el objeto
        {

            try 
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }


                //validar si existe o no el id Villa (padre)
                if (await _villaRepo.GetOne(v => v.Id == updateDto.VillaId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "No existe la clave foranea id de la villa");
                    return BadRequest(ModelState);
                }

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);

                await _numeroRepo.Update(modelo); //accede al repo
                _apiResponse.statusCode=HttpStatusCode.NoContent;

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }
            

            return Ok(_apiResponse);
        }

        

    }
}
