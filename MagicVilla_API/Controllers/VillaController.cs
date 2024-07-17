using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        //endpoint
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.VillaList);
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

            var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);

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
            if (VillaStore.VillaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
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
            villaDto.Id = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1; //buscar el ultimo id en la lista
            VillaStore.VillaList.Add(villaDto); //agregar los datos a la lista

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
            var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound(); //404
            }

            VillaStore.VillaList.Remove(villa); //elimina de la lista
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

            var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
            villa.Nombre = villaDto.Nombre;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("id:int")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto) //recibe todo el objeto
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest(); //400
            }

            var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);

            patchDto.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }
}
