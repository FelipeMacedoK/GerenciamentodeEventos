using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("feedback")]
    public class Feedback
    {
        [Column("idfeedback")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Feedback")]
        public int IdFeedback { get; set; }

        [Column("nota")]
        [Required(ErrorMessage = "A nota é obrigatória.")]
        [Range(1, 10, ErrorMessage = "A nota deve ser entre 1 e 10.")]
        [Display(Name = "Nota")]
        [DataType(DataType.Text)]
        public int Nota { get; set; }

        [Column("comentario")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "O comentário deve ter entre 10 e 500 caracteres.")]
        [Display(Name = "Comentário")]
        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; } = string.Empty;

        [Column("datafeedback")]
        [Display(Name = "Data do Feedback")]
        [DataType(DataType.DateTime)]
        public DateTime DataFeedback { get; set; } = DateTime.Now;

        [Column("idevento")]
        [ForeignKey("IdEvento")]
        public virtual Evento? Evento { get; set; }

        [Column("idpessoa")]
        [ForeignKey("IdPessoa")]
        public virtual Pessoa? Pessoa { get; set; }

    }
}
