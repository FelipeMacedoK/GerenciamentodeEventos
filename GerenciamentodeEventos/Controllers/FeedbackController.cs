using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GerenciamentodeEventos.Data;
using GerenciamentodeEventos.Model;

namespace GerenciamentodeEventos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFeedback()
        {
            var feedbacks = await _context.Feedback
                .Include(f => f.Evento)
                .Include(f => f.Pessoa)
                .Select(f => new
                {
                    f.IdFeedback,
                    f.Nota,
                    f.Comentario,
                    DataFeedback = f.DataFeedback.ToString("dd/MM/yyyy HH:mm"),
                    Evento = f.Evento == null ? null : new
                    {
                        f.Evento.IdEvento,
                        f.Evento.Nome,
                        f.Evento.Descricao,
                        DataHora = f.Evento.DataHora.ToString("dd/MM/yyyy HH:mm")
                    },
                    Pessoa = f.Pessoa == null ? null : new
                    {
                        f.Pessoa.IdPessoa,
                        f.Pessoa.Nome,
                        f.Pessoa.Email,
                        f.Pessoa.Telefone
                    }
                })
                .ToListAsync();

            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFeedback(int id)
        {
            var feedback = await _context.Feedback
                .Include(f => f.Evento)
                .Include(f => f.Pessoa)
                .Where(f => f.IdFeedback == id)
                .Select(f => new
                {
                    f.IdFeedback,
                    f.Nota,
                    f.Comentario,
                    DataFeedback = f.DataFeedback.ToString("dd/MM/yyyy HH:mm"),
                    Evento = f.Evento == null ? null : new
                    {
                        f.Evento.IdEvento,
                        f.Evento.Nome,
                        f.Evento.Descricao,
                        DataHora = f.Evento.DataHora.ToString("dd/MM/yyyy HH:mm")
                    },
                    Pessoa = f.Pessoa == null ? null : new
                    {
                        f.Pessoa.IdPessoa,
                        f.Pessoa.Nome,
                        f.Pessoa.Email,
                        f.Pessoa.Telefone
                    }
                })
                .FirstOrDefaultAsync();

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        [HttpPut]
        public async Task<IActionResult> PutFeedback(int id, Feedback feedback)
        {
            if (id != feedback.IdFeedback)
            {
                return BadRequest();
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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
        public async Task<ActionResult<Feedback>> PostFeedback(int eventoId, int pessoaId, int nota, string comentario)
        {
            if (!_context.Evento.Any(e => e.IdEvento == eventoId))
            {
                return NotFound(new { Message = "Evento não encontrado." });
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == pessoaId))
            {
                return NotFound(new { Message = "Pessoa não encontrada." });
            }

            var novoFeedback = new Feedback
            {
                Evento = await _context.Evento.FindAsync(eventoId),
                Pessoa = await _context.Pessoa.FindAsync(pessoaId),
                Nota = nota,
                Comentario = comentario,
                DataFeedback = DateTime.UtcNow
            };

            _context.Feedback.Add(novoFeedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = novoFeedback.IdFeedback }, novoFeedback);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedback.Any(e => e.IdFeedback == id);
        }
    }
}
