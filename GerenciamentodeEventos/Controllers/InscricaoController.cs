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

        [HttpPut]
        public async Task<IActionResult> PutInscricao(int id, Inscricao inscricao)
        {
            if (id != inscricao.IdInscricao)
            {
                return BadRequest();
            }

            _context.Entry(inscricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoExists(id))
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
        public async Task<ActionResult<Inscricao>> PostInscricao(int eventoId, int pessoaId)
        {
            if (!_context.Evento.Any(e => e.IdEvento == eventoId))
            {
                return NotFound(new { Message = "Evento não encontrado." });
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == pessoaId))
            {
                return NotFound(new { Message = "Pessoa não encontrada." });
            }

            var ultimoSequencial = await _context.Inscricao
                .Where(i => i.Evento != null && i.Evento.IdEvento == eventoId)
                .OrderByDescending(i => i.Sequencial)
                .Select(i => i.Sequencial)
                .FirstOrDefaultAsync();

            var novaInscricao = new Inscricao
            {
                Evento = await _context.Evento.FindAsync(eventoId),
                Pessoa = await _context.Pessoa.FindAsync(pessoaId),
                DataInscricao = DateTime.UtcNow,
                Sequencial = ultimoSequencial + 1
            };

            _context.Inscricao.Add(novaInscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = novaInscricao.IdInscricao }, novaInscricao);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscricao(int id)
        {
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
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
