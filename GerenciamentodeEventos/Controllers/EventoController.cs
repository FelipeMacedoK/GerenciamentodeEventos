using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;
using GerenciamentodeEventos.Model.eNum;

namespace GerenciamentodeEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetEvento()
        {
            var eventos = await _context.Evento
                .Include(e => e.Categoria)
                .Include(e => e.Local)
                .Select(e => new
                {
                    e.IdEvento,
                    e.Nome,
                    e.Descricao,
                    DataHora = e.DataHora.ToString("dd/MM/yyyy HH:mm"),
                    e.Capacidade,
                    e.Valor,
                    SituacaoInscricao = e.SituacaoInscricao.ToString(),
                    Categoria = e.Categoria == null ? null : new
                    {
                        e.Categoria.IdCategoria,
                        e.Categoria.Nome,
                        e.Categoria.Descricao
                    },
                    Local = e.Local == null ? null : new
                    {
                        e.Local.IdLocal,
                        e.Local.Logradouro,
                        e.Local.Numero,
                        e.Local.Bairro,
                        e.Local.Cidade,
                        e.Local.Estado
                    }
                })
                .ToListAsync();

            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEvento(int id)
        {
            var evento = await _context.Evento
                .Include(e => e.Categoria)
                .Include(e => e.Local)
                .Where(e => e.IdEvento == id)
                .Select(e => new
                {
                    e.IdEvento,
                    e.Nome,
                    e.Descricao,
                    DataHora = e.DataHora.ToString("dd/MM/yyyy HH:mm"),
                    e.Capacidade,
                    e.Valor,
                    SituacaoInscricao = e.SituacaoInscricao.ToString(),
                    Categoria = e.Categoria == null ? null : new
                    {
                        e.Categoria.IdCategoria,
                        e.Categoria.Nome,
                        e.Categoria.Descricao
                    },
                    Local = e.Local == null ? null : new
                    {
                        e.Local.IdLocal,
                        e.Local.Logradouro,
                        e.Local.Numero,
                        e.Local.Bairro,
                        e.Local.Cidade,
                        e.Local.Estado
                    }
                })
                .FirstOrDefaultAsync();

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventoExistente = await _context.Evento
                .Include(e => e.Categoria)
                .Include(e => e.Local)
                .FirstOrDefaultAsync(e => e.IdEvento == id);

            if (eventoExistente == null)
            {
                return NotFound("Evento não encontrado.");
            }

            if (!_context.Categoria.Any(c => c.IdCategoria == evento.IdCategoria))
            {
                return NotFound("Categoria não encontrada.");
            }

            if (!_context.Local.Any(l => l.IdLocal == evento.IdLocal))
            {
                return NotFound("Local não encontrado.");
            }

            eventoExistente.Nome = evento.Nome;
            eventoExistente.Descricao = evento.Descricao;
            eventoExistente.DataHora = evento.DataHora;
            eventoExistente.Capacidade = evento.Capacidade;
            eventoExistente.Valor = evento.Valor;
            eventoExistente.SituacaoInscricao = evento.SituacaoInscricao;
            eventoExistente.Categoria = await _context.Categoria.FindAsync(evento.IdCategoria);
            eventoExistente.Local = await _context.Local.FindAsync(evento.IdLocal);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
                {
                    return NotFound("Evento não encontrado durante a atualização.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> PostEvento([FromBody] Evento evento)
        {
            if (!_context.Categoria.Any(c => c.IdCategoria == evento.IdCategoria))
            {
                return NotFound(new { Message = "Categoria não encontrada." });
            }

            if (!_context.Local.Any(l => l.IdLocal == evento.IdLocal))
            {
                return NotFound(new { Message = "Local não encontrado." });
            }

            if (evento.SituacaoInscricao == 0)
            {
                evento.SituacaoInscricao = SituacaoInscricao.Privada;
            }

            _context.Evento.Add(evento);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEvento", new { id = evento.IdEvento }, evento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound("Evento não encontrado.");
            }

            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.IdEvento == id);
        }
    }
}
