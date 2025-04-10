using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizadores()
        {
            var organizadores = await _context.Organizador.ToListAsync();
            return Ok(organizadores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganizador(int id)
        {
            var organizador = await _context.Organizador.FindAsync(id);
            if (organizador == null)
            {
                return NotFound();
            }
            return Ok(organizador);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganizador([FromBody] Organizador organizador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Organizador.Add(organizador);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrganizador), new { id = organizador.Id }, organizador);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizador(int id, [FromBody] Organizador organizador)
        {
            if (id != organizador.Id)
            {
                return BadRequest();
            }

            _context.Entry(organizador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Organizador.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizador(int id)
        {
            var organizador = await _context.Organizador.FindAsync(id);
            if (organizador == null)
            {
                return NotFound();
            }

            _context.Organizador.Remove(organizador);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}