using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GerenciamentodeEventos.Model.eNum;

namespace GerenciamentodeEventos.Model
{
    [Table("evento")]
    public class Evento
    {
        [Column("idevento")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Evento")]
        public int IdEvento { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "O nome do evento é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do evento deve ter de 3 a 100 caracteres.")]
        [Display(Name = "Nome do Evento")]
        [DataType(DataType.Text)]
        public string Nome { get; set; } = string.Empty;

        [Column("descricao")]
        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição do evento deve ter de 10 a 500 caracteres.")]
        [Display(Name = "Descrição do Evento")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

        [Column("datahora")]
        [Required(ErrorMessage = "A data e hora do evento são obrigatórias.")]
        [Display(Name = "Data e Hora do Evento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Column("capacidade")]
        [Range(1, 10000, ErrorMessage = "A capacidade de participantes deve ser entre 1 e 10000.")]
        [Display(Name = "Capacidade de Participantes")]
        [DataType(DataType.Text)]
        public int Capacidade { get; set; }

        [Column("valor")]
        [Range(0, 10000, ErrorMessage = "O valor do evento deve ser entre 0 e 10000.")]
        [Display(Name = "Valor do Evento")]
        [DataType(DataType.Currency)]
        public float Valor { get; set; } = 0;

        [Column("situacaoinscricao")]
        [Required(ErrorMessage = "A situação da inscrição é obrigatória.")]
        [Display(Name = "Situação da Inscrição")]
        public SituacaoInscricao SituacaoInscricao { get; set; } = SituacaoInscricao.Privada;

        [Column("idcategoria")]
        [Required]
        [ForeignKey("Categoria")]
        [Display(Name = "ID da Categoria")]
        public int IdCategoria { get; set; }

        public virtual Categoria? Categoria { get; set; }

        [Column("idlocal")]
        [Required]
        [ForeignKey("Local")]
        [Display(Name = "ID do Local")]
        public int IdLocal { get; set; }

        public virtual Local? Local { get; set; }
    }
}