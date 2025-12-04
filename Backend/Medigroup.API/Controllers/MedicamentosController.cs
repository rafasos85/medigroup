using Medigroup.API.Data;
using Medigroup.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medigroup.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MedicamentosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo para obtener los medicamentos con filtros opcionales
        /// </summary>
        /// <param name="search"></param>
        /// <param name="categoria"></param>
        /// <param name="fechaExpiracion"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos(string? search, string? categoria, DateTime? fechaExpiracion)
        {
            var query = _context.Medicamentos.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(m => m.Nombre.Contains(search));
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(m => m.Categoria == categoria);
            }

            if (fechaExpiracion.HasValue)
            {
                query = query.Where(m => m.FechaExpiracion.Date == fechaExpiracion.Value.Date);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Metodo para obtener un medicamento por su ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        /// <summary>
        /// Metodo para crear un nuevo medicamento
        /// </summary>
        /// <param name="medicamento"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicamento", new { id = medicamento.Id }, medicamento);
        }

        /// <summary>
        /// Metodo para actualizar un medicamento existente
        /// </summary>
        /// <param name="id"></param>
        /// <param name="medicamento"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (id != medicamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Metodo para eliminar un medicamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// verifica si un medicamento existe
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.Id == id);
        }
    }
}
