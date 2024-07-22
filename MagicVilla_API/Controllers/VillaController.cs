using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {   
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }


        //endpoint
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            //_logger.LogInformation("Obteniendo villas");
            //_logger.LogError("error mostrar");

            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList)); 
        }

        [HttpGet("id", Name = "GetVilla")] //"id:int"
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest(); //400 
            }

            //var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id); //traer un registro

            if (villa == null)
            {
                return NotFound(); //404 no se encontro ningun registo
            }

            return Ok(_mapper.Map<VillaDto>(villa)); //200 todo ok
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CreateVilla([FromBody] VillaCreateDto createDto)  //from body indica que vamos a recibir datos VillaDto tipo de dato que vamos a recibir y le ponemos el nombre
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            //validar que no se repitan nombres de villas
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Iguales", "El nombre de la villa ya existe"); //Nombre del error, mensaje a mostrar
                return BadRequest(ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(); //no nos estan enviado datos
            }

            Villa modelo = _mapper.Map<Villa>(createDto);
            
            //Lo reemplaza el mapper
            /*
            Villa modelo = new()
            {
                Nombre = createDto.Nombre,
                Detalle = createDto.Detalle,
                ImagenUrl = createDto.ImagenUrl,
                Ocupantes = createDto.Ocupantes,
                Tarifa = createDto.Tarifa,
                MetrosCuadrados = createDto.MetrosCuadrados,
                Amenidad = createDto.Amenidad
            };*/

            //agregar el registo a bd
            await _db.Villas.AddAsync(modelo); //INSERT
            await _db.SaveChangesAsync(); //guardar bd

            return CreatedAtRoute("GetVilla", new { id = modelo.Id }, modelo); //el modelo es el que guardamos contiene ya el id
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("id:int")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest(); //400
            }
            //ver si el id encuentra algo en la lista
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return NotFound(); //404
            }

            _db.Villas.Remove(villa); //remove no es async
            await _db.SaveChangesAsync(); //guardar bd
            return NoContent(); //204
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("id:int")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) //recibe todo el objeto
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest(); //400
            }

            Villa modelo = _mapper.Map<Villa>(updateDto);
            

            _db.Villas.Update(modelo); //update
            await _db.SaveChangesAsync(); //guardar bd

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("id:int")]
        public async Task< IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest(); //400
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id); //registro actual que se va a modificar

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
            
            /* //reemplazado por mapper
            VillaUpdateDto villaDto = new() //el modelo lo llenamos con la info del actual
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };*/

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);

            /* //reemplazado por el mapper
            Villa modelo = new() //luego de pasar el patch que contiene lo unico que se va a modificar
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };*/

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
