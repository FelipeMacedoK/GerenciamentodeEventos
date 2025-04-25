using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InscricaoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetInscricao()
        {
            var inscricoes = await _context.Inscricao
                .Include(i => i.Evento)
                .Include(i => i.Pessoa)
                .Select(i => new
                {
                    i.IdInscricao,
                    DataInscricao = i.DataInscricao.ToString("dd/MM/yyyy HH:mm"),
                    i.Sequencial,
                    Evento = i.Evento == null ? null : new
                    {
                        i.Evento.IdEvento,
                        i.Evento.Nome,
                        i.Evento.Descricao,
                        DataHora = i.Evento.DataHora.ToString("dd/MM/yyyy HH:mm")
                    },
                    Pessoa = i.Pessoa == null ? null : new
                    {
                        i.Pessoa.IdPessoa,
                        i.Pessoa.Nome,
                        i.Pessoa.Email,
                        i.Pessoa.Telefone
                    }
                })
                .ToListAsync();

            return Ok(inscricoes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetInscricao(int id)
        {
            var inscricao = await _context.Inscricao
                .Include(i => i.Evento)
                .Include(i => i.Pessoa)
                .Where(i => i.IdInscricao == id)
                .Select(i => new
                {
                    i.IdInscricao,
                    DataInscricao = i.DataInscricao.ToString("dd/MM/yyyy HH:mm"),
                    i.Sequencial,
                    Evento = i.Evento == null ? null : new
                    {
                        i.Evento.IdEvento,
                        i.Evento.Nome,
                        i.Evento.Descricao,
                        DataHora = i.Evento.DataHora.ToString("dd/MM/yyyy HH:mm")
                    },
                    Pessoa = i.Pessoa == null ? null : new
                    {
                        i.Pessoa.IdPessoa,
                        i.Pessoa.Nome,
                        i.Pessoa.Email,
                        i.Pessoa.Telefone
                    }
                })
                .FirstOrDefaultAsync();

            if (inscricao == null)
            {
                return NotFound();
            }

            return Ok(inscricao);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscricao(int id, [FromBody] Inscricao inscricao)
        {
            if (id != inscricao.IdInscricao)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inscricaoExistente = await _context.Inscricao
                .Include(i => i.Evento)
                .Include(i => i.Pessoa)
                .FirstOrDefaultAsync(i => i.IdInscricao == id);

            if (inscricaoExistente == null)
            {
                return NotFound("Inscrição não encontrada.");
            }

            if (!_context.Evento.Any(e => e.IdEvento == inscricao.IdEvento))
            {
                return NotFound("Evento não encontrado.");
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == inscricao.IdPessoa))
            {
                return NotFound("Pessoa não encontrada.");
            }

            inscricaoExistente.Evento = await _context.Evento.FindAsync(inscricao.IdEvento);
            inscricaoExistente.Pessoa = await _context.Pessoa.FindAsync(inscricao.IdPessoa);
            inscricaoExistente.DataInscricao = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoExists(id))
                {
                    return NotFound("Inscrição não encontrada durante a atualização.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Inscricao>> PostInscricao([FromBody] Inscricao inscricao)
        {
            if (!_context.Evento.Any(e => e.IdEvento == inscricao.IdEvento))
            {
                return NotFound(new { Message = "Evento não encontrado." });
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == inscricao.IdPessoa))
            {
                return NotFound(new { Message = "Pessoa não encontrada." });
            }

            var ultimoSequencial = await _context.Inscricao
                .Where(i => i.IdEvento == inscricao.IdEvento)
                .OrderByDescending(i => i.Sequencial)
                .Select(i => i.Sequencial)
                .FirstOrDefaultAsync();

            inscricao.Sequencial = ultimoSequencial + 1;
            inscricao.DataInscricao = DateTime.UtcNow;

            _context.Inscricao.Add(inscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = inscricao.IdInscricao }, inscricao);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscricao(int id)
        {
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound("Inscrição não encontrada.");
            }

            _context.Inscricao.Remove(inscricao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscricaoExists(int id)
        {
            return _context.Inscricao.Any(e => e.IdInscricao == id);
        }
    }
}