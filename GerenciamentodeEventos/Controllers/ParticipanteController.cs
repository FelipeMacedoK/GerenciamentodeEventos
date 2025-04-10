using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipanteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ParticipanteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipantes()
        {
            var participantes = await _context.Participante.Include(p => p.Evento).ToListAsync();
            return Ok(participantes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetParticipante(int id)
        {
            var participante = await _context.Participante.Include(p => p.Evento).FirstOrDefaultAsync(p => p.Id == id);
            if (participante == null)
            {
                return NotFound();
            }
            return Ok(participante);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipante([FromBody] Participante participante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Participante.Add(participante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParticipante), new { id = participante.Id }, participante);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, [FromBody] Participante participante)
        {
            if (id != participante.Id)
            {
                return BadRequest();
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Participante.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participante.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participante.Remove(participante);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}