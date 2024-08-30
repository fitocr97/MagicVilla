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
    public class VillaController : ControllerBase
    {   
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _apiResponse = new APIResponse();
        }

        //getall
        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetVillas() //RETORNA TIPO APIRESPONSE antes ienumerable
        {
            try 
            {
                IEnumerable<Villa> villaList = await _villaRepo.GetAll();

                _apiResponse.Result = _mapper.Map<IEnumerable<VillaDto>>(villaList); //agrega datos al response
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
        [HttpGet("{id:int}", Name = "GetVilla")] //"id:int" 
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<APIResponse>> GetVilla(int id)
        {
            try 
            {
                if (id == 0)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                var villa = await _villaRepo.GetOne(v => v.Id == id); //le enviamos el filtro por id

                if (villa == null)
                {
                    _apiResponse.statusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccessful = false;
                    return NotFound(_apiResponse);
                }

                _apiResponse.Result = _mapper.Map<VillaDto>(villa); //agrega datos al response
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
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto createDto)  //from body indica que vamos a recibir datos VillaDto tipo de dato que vamos a recibir y le ponemos el nombre
        {

            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //validar que no se repitan nombres de villas
                if (await _villaRepo.GetOne(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "El nombre de la villa ya existe"); //Nombre del error, mensaje a mostrar
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto); //no nos estan enviado datos
                }

                Villa modelo = _mapper.Map<Villa>(createDto);

                modelo.FechaCreacion = DateTime.Now;
                modelo.FechaActualizacio = DateTime.Now;

                await _villaRepo.Create(modelo); //INSERT
                _apiResponse.Result = modelo;
                _apiResponse.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = modelo.Id }, _apiResponse); //se pasa el response
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccessful = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() }; //lista de errores
            }

            return _apiResponse;
        }

        //Delete¿
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id) //aqui no se puede poner APIResponse, interface no puede llevar un tipo
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
                var villa = await _villaRepo.GetOne(v => v.Id == id);

                if (villa == null)
                {
                    _apiResponse.statusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                await _villaRepo.Delete(villa);
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
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) //recibe todo el objeto
        {

            try 
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                Villa modelo = _mapper.Map<Villa>(updateDto);

                await _villaRepo.Update(modelo); 
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

        //patch
        [HttpPatch("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task< IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            try 
            {
                if (patchDto == null || id == 0)
                {
                    _apiResponse.statusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccessful = false;
                    return BadRequest(_apiResponse);
                }

                var villa = await _villaRepo.GetOne(v => v.Id == id, tracked: false); //tracked false

                VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);


                if (villa == null) return BadRequest();

                patchDto.ApplyTo(villaDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Villa modelo = _mapper.Map<Villa>(villaDto);

                await _villaRepo.Update(modelo);
                _apiResponse.statusCode = HttpStatusCode.NoContent;

                return NoContent();

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
