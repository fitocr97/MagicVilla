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
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        //endpoint
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obteniendo villas");
            _logger.LogError("error mostrar");

            return Ok(_db.Villas.ToList()); //select * from
        }

        [HttpGet("id", Name = "GetVilla")] //"id:int"
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest(); //400 
            }

            //var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id); //traer un registro

            if (villa == null)
            {
                return NotFound(); //404 no se encontro ningun registo
            }

            return Ok(villa); //200 todo ok
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDto)  //from body indica que vamos a recibir datos VillaDto tipo de dato que vamos a recibir y le ponemos el nombre
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); //400
            }

            //validar que no se repitan nombres de villas
            if (_db.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("Iguales", "El nombre de la villa ya existe"); //Nombre del error, mensaje a mostrar
                return BadRequest(ModelState);
            }

            if (villaDto == null)
            {
                return BadRequest(); //no nos estan enviado datos
            }

            if (villaDto.Id > 0) //estamos agregando un nuevo, no estamos solitando un id por eso hay un error
            {
                return StatusCode(StatusCodes.Status500InternalServerError); //
            }

            //creamos un nuevo modelo en base a la villa
            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            //agregar el registo a bd
            _db.Villas.Add(modelo); //INSERT
            _db.SaveChanges(); //guardar bd

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("id:int")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest(); //400
            }
            //ver si el id encuentra algo en la lista
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound(); //404
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges(); //guardar bd
            return NoContent(); //204
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("id:int")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto) //recibe todo el objeto
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest(); //400
            }

            // creamos un nuevo modelo en base a la villa
            Villa modelo = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUrl = villaDto.ImagenUrl,
                Ocupantes = villaDto.Ocupantes,
                Tarifa = villaDto.Tarifa,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                Amenidad = villaDto.Amenidad
            };

            _db.Villas.Update(modelo); //update
            _db.SaveChanges(); //guardar bd

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("id:int")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest(); //400
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id); //registro actual que se va a modificar

            VillaDto villaDto = new() //el modelo lo llenamos con la info del actual
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                ImagenUrl = villa.ImagenUrl,
                Ocupantes = villa.Ocupantes,
                Tarifa = villa.Tarifa,
                MetrosCuadrados = villa.MetrosCuadrados,
                Amenidad = villa.Amenidad
            };

            if (villa == null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            };

            _db.Villas.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
