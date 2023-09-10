using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Security;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IMascotaRepository mascotaRepository;

        public MascotaController( IMapper mapper, IMascotaRepository mascotaRepository)
        {
            this.mapper = mapper;
            this.mascotaRepository = mascotaRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Mascota>>> Get()
        {
            try
            {
                var mascotas = await mascotaRepository.GetListMascotas();
                if (mascotas is null || mascotas.Count == 0)
                {
                    return BadRequest("No hay Mascotas registrados");
                }

                var listMascotasMapper = mapper.Map<IEnumerable<MascotaDTO>>(mascotas);

                return Ok(listMascotasMapper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var mascota = await mascotaRepository.GetMascota(id);
            if (mascota == null)
            {
                return BadRequest("No existe esa mascota");
            }

            var mascotaDTO = mapper.Map<MascotaDTO>(mascota);
            return Ok(mascotaDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Mascota>> Post(MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = mapper.Map<Mascota>(mascotaDTO);
                
                mascota.FechaCreacion = DateTime.Now;

               var mascotaItem = await mascotaRepository.AddMascota(mascota);

                var mascotaItemDTO = mapper.Map<MascotaDTO>(mascotaItem);

                return CreatedAtAction("Get", new { id = mascota.Id }, mascotaItemDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                //comprobar que ese id exista en la base de datos
                var mascota = await mascotaRepository.GetMascota(id);

                if (mascota == null) return NotFound();

                await mascotaRepository.DeleteMascota(mascota);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = mapper.Map<Mascota>(mascotaDTO);
                //Comprobar que el id que pasen sea el mismo que en el body
                if (id != mascota.Id)
                {
                    return BadRequest("Id invalido");
                }
                //comprobar que ese id exista en la base de datos
                var mascotaItem = await mascotaRepository.GetMascota(id);

                if (mascotaItem == null)
                {
                    return BadRequest("El Cliente no existe");
                }

                await mascotaRepository.UpdateMascota(mascota);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
