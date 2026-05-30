using ApiInteligenteTareas.Data;
using ApiInteligenteTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiInteligenteTareas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas(
        string? estado,
        string? prioridad,
        DateTime? fechaInicio,
        DateTime? fechaFin)
    {
        var query = _context.Tareas.AsQueryable();

    // Validar rango de fechas
        if (fechaInicio.HasValue && fechaFin.HasValue &&
            fechaInicio > fechaFin)
        {
            return BadRequest(
                "fechaInicio no puede ser mayor que fechaFin");
        }

    // Filtrar por estado
        if (!string.IsNullOrWhiteSpace(estado))
        {
            if (!Enum.TryParse<EstadoTarea>(
                    estado,
                    true,
                    out var estadoEnum))
            {
                return BadRequest("Estado inválido");
            }

            query = query.Where(t => t.Estado == estadoEnum);
        }

    // Filtrar por prioridad
        if (!string.IsNullOrWhiteSpace(prioridad))
        {
            if (!Enum.TryParse<PrioridadTarea>(
                    prioridad,
                    true,
                    out var prioridadEnum))
            {
                return BadRequest("Prioridad inválida");
            }

            query = query.Where(t => t.Prioridad == prioridadEnum);
        }

    // Filtrar por fechas
        if (fechaInicio.HasValue)
        {
            query = query.Where(
                t => t.FechaVencimiento >= fechaInicio.Value);
        }

        if (fechaFin.HasValue)
        {
            query = query.Where(
                t => t.FechaVencimiento <= fechaFin.Value);
        }

        return await query.ToListAsync();
    }       

        // GET: api/tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            return tarea;
        }

        // POST: api/tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.IsDefined(typeof(EstadoTarea), tarea.Estado))
                return BadRequest("Estado inválido.");

            if (!Enum.IsDefined(typeof(PrioridadTarea), tarea.Prioridad))
                return BadRequest("Prioridad inválida.");

            if (tarea.FechaVencimiento.Date < DateTime.Today)
                return BadRequest(
                    "La fecha de vencimiento no puede ser menor a la fecha actual.");

            tarea.FechaCreacion = DateTime.Now;

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTarea),
                new { id = tarea.Id },
                tarea);
        }

        // PUT: api/tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, Tarea tarea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != tarea.Id)
                return BadRequest("El ID de la URL no coincide con el ID enviado.");

            if (!Enum.IsDefined(typeof(EstadoTarea), tarea.Estado))
                return BadRequest("Estado inválido.");

            if (!Enum.IsDefined(typeof(PrioridadTarea), tarea.Prioridad))
                return BadRequest("Prioridad inválida.");

            if (tarea.FechaVencimiento.Date < DateTime.Today)
                return BadRequest(
                    "La fecha de vencimiento no puede ser menor a la fecha actual.");

            var tareaExistente = await _context.Tareas.FindAsync(id);

            if (tareaExistente == null)
                return NotFound();

            tareaExistente.Titulo = tarea.Titulo;
            tareaExistente.Descripcion = tarea.Descripcion;
            tareaExistente.Estado = tarea.Estado;
            tareaExistente.Prioridad = tarea.Prioridad;
            tareaExistente.FechaVencimiento = tarea.FechaVencimiento;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
                return NotFound();

            _context.Tareas.Remove(tarea);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}