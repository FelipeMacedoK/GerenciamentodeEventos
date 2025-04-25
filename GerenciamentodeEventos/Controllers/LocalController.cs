using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LocalController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Local>>> GetLocal()
        {
            return await _context.Local.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Local>> GetLocal(int id)
        {
            var local = await _context.Local.FindAsync(id);

            if (local == null)
            {
                return NotFound();
            }

            return local;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocal(int id, [FromBody] Local local)
        {
            if (id != local.IdLocal)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var localExistente = await _context.Local.FindAsync(id);
            if (localExistente == null)
            {
                return NotFound("Local não encontrado.");
            }

            localExistente.Logradouro = local.Logradouro;
            localExistente.Numero = local.Numero;
            localExistente.Bairro = local.Bairro;
            localExistente.Cidade = local.Cidade;
            localExistente.Estado = local.Estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalExists(id))
                {
                    return NotFound("Local não encontrado durante a atualização.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Local>> PostLocal(Local local)
        {
            _context.Local.Add(local);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocal", new { id = local.IdLocal }, local);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocal(int id)
        {
            var local = await _context.Local.FindAsync(id);
            if (local == null)
            {
                return NotFound("Local não encontrado.");
            }

            _context.Local.Remove(local);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocalExists(int id)
        {
            return _context.Local.Any(e => e.IdLocal == id);
        }
    }
}