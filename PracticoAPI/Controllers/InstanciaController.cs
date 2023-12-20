using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Practico.Data;
using PracticoAPI.Models;

using Microsoft.AspNetCore.Cors;

namespace PracticoAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstanciaController : ControllerBase
    {
        public readonly ApplicationDbContext _dbcontext;

        public InstanciaController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Instancia> lista = new List<Instancia>();

            try
            {
                lista = _dbcontext.Instancias.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{IdInstancia:int}")]
        public IActionResult Obtener(int IdInstancia)
        {
            Instancia oInstancia = _dbcontext.Instancias.Find(IdInstancia);

            if (oInstancia == null) {
                return BadRequest("Instancia no encontrada.");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = oInstancia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oInstancia });
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar ([FromBody] Instancia objeto)
        {
            try
            {
                _dbcontext.Instancias.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Instancia objeto)
        {
            Instancia oInstancia = _dbcontext.Instancias.Find(objeto.Id);

            if (oInstancia == null)
            {
                return BadRequest("Instancia no encontrada.");
            }

            try
            {
                oInstancia.Nombre = objeto.Nombre is null ? oInstancia.Nombre : objeto.Nombre;
                oInstancia.URL = objeto.URL is null ? oInstancia.URL : objeto.URL;
                oInstancia.PaletaColor = objeto.PaletaColor;
                oInstancia.TipoRegistro = objeto.TipoRegistro;
                oInstancia.Tematica = objeto.Tematica;
                oInstancia.Estado = objeto.Estado;

                _dbcontext.Instancias.Update(oInstancia);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdInstancia:int}")]
        public IActionResult Eliminar(int IdInstancia)
        {
            Instancia oInstancia = _dbcontext.Instancias.Find(IdInstancia);

            if (oInstancia == null)
            {
                return BadRequest("Instancia no encontrada.");
            }

            try
            {
                _dbcontext.Instancias.Remove(oInstancia);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }
}
