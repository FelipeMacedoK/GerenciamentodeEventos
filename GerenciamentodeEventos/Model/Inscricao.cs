using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("inscricao")]
    public class Inscricao
    {
        [Column("idinscricao")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID da Inscrição")]
        public int IdInscricao { get; set; }

        [Column("datainscricao")]
        [Required(ErrorMessage = "A data da inscrição é obrigatória.")]
        [Display(Name = "Data da Inscrição")]
        [DataType(DataType.DateTime)]
        public DateTime DataInscricao { get; set; } = DateTime.Now;

        [Column("numeroinscricao")]
        [Required]
        [Display(Name = "Número da Inscrição")]
        public int Sequencial { get; set; }

        [Column("idevento")]
        [ForeignKey("IdEvento")]
        public virtual Evento? Evento { get; set; }

        [Column("idpessoa")]
        [ForeignKey("IdPessoa")]
        public virtual Pessoa? Pessoa { get; set; }

    }
}
