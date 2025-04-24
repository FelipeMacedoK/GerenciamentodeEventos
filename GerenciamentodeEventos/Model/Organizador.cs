using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("organizador")]
    public class Organizador
    {
        [Column("idorganizador")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Organizador")]
        public int IdOrganizador { get; set; }

        [Column("biografia")]
        [Display(Name = "Biografia do Organizador")]
        [DataType(DataType.MultilineText)]
        public string? Biografia { get; set; }

        [Column("idpessoa")]
        [Required]
        [Display(Name = "ID da Pessoa")]
        public int IdPessoa { get; set; }

        [Column("idevento")]
        [Required]
        [Display(Name = "ID do Evento")]
        public int IdEvento { get; set; }

        [ForeignKey("IdPessoa")]
        public virtual Pessoa? Pessoa { get; set; }

        [ForeignKey("IdEvento")]
        public virtual Evento? Evento { get; set; }
    }
}
