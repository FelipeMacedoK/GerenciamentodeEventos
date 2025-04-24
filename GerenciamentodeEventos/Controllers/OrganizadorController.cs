using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizadorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrganizadorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetOrganizador()
        {
            var organizadores = await _context.Organizador
                .Include(o => o.Evento)
                .Include(o => o.Pessoa)
                .Select(o => new
                {
                    o.IdOrganizador,
                    o.Biografia,
                    Evento = o.Evento == null ? null : new
                    {
                        o.Evento.IdEvento,
                        o.Evento.Nome,
                        o.Evento.Descricao,
                        DataHora = o.Evento.DataHora.ToString("dd/MM/yyyy HH:mm")
                    },
                    Pessoa = o.Pessoa == null ? null : new
                    {
                        o.Pessoa.IdPessoa,
                        o.Pessoa.Nome,
                        o.Pessoa.Email,
                        o.Pessoa.Telefone
                    }
                })
                .ToListAsync();

            return Ok(organizadores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organizador>> GetOrganizador(int id)
        {
            var organizador = await _context.Organizador.FindAsync(id);

            if (organizador == null)
            {
                return NotFound();
            }

            return organizador;
        }

        [HttpPut]
        public async Task<IActionResult> PutOrganizador(int id, Organizador organizador)
        {
            if (id != organizador.IdOrganizador)
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
                if (!OrganizadorExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Organizador>> PostOrganizador(Organizador organizador)
        {
            if (!_context.Evento.Any(e => e.IdEvento == organizador.IdEvento))
            {
                return NotFound(new { Message = "Evento não encontrado." });
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == organizador.IdPessoa))
            {
                return NotFound(new { Message = "Pessoa não encontrada." });
            }

            _context.Organizador.Add(organizador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganizador", new { id = organizador.IdOrganizador }, organizador);
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

        private bool OrganizadorExists(int id)
        {
            return _context.Organizador.Any(e => e.IdOrganizador == id);
        }
    }
}