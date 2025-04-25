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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, [FromBody] Feedback feedback)
        {
            if (id != feedback.IdFeedback)
            {
                return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var feedbackExistente = await _context.Feedback
                .Include(f => f.Evento)
                .Include(f => f.Pessoa)
                .FirstOrDefaultAsync(f => f.IdFeedback == id);

            if (feedbackExistente == null)
            {
                return NotFound("Feedback não encontrado.");
            }

            if (!_context.Evento.Any(e => e.IdEvento == feedback.IdEvento))
            {
                return NotFound("Evento não encontrado.");
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == feedback.IdPessoa))
            {
                return NotFound("Pessoa não encontrada.");
            }

            feedbackExistente.Nota = feedback.Nota;
            feedbackExistente.Comentario = feedback.Comentario;
            feedbackExistente.DataFeedback = DateTime.UtcNow;
            feedbackExistente.Evento = await _context.Evento.FindAsync(feedback.IdEvento);
            feedbackExistente.Pessoa = await _context.Pessoa.FindAsync(feedback.IdPessoa);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                {
                    return NotFound("Feedback não encontrado durante a atualização.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback([FromBody] Feedback feedback)
        {
            if (!_context.Evento.Any(e => e.IdEvento == feedback.IdEvento))
            {
                return NotFound(new { Message = "Evento não encontrado." });
            }

            if (!_context.Pessoa.Any(p => p.IdPessoa == feedback.IdPessoa))
            {
                return NotFound(new { Message = "Pessoa não encontrada." });
            }

            feedback.Evento = await _context.Evento.FindAsync(feedback.IdEvento);
            feedback.Pessoa = await _context.Pessoa.FindAsync(feedback.IdPessoa);
            feedback.DataFeedback = DateTime.UtcNow;

            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFeedback", new { id = feedback.IdFeedback }, feedback);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return NotFound("Feedback não encontrado.");
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
