using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GerenciamentodeEventos.Model
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Evento")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do evento é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do evento deve ter de 3 a 100 caracteres.")]
        [Display(Name = "Nome do Evento")]
        [DataType(DataType.Text)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O local do evento é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O local do evento deve ter de 3 a 100 caracteres.")]
        [Display(Name = "Local do Evento")]
        [DataType(DataType.Text)]
        public string Local { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data e hora do evento são obrigatórias.")]
        [Display(Name = "Data e Hora do Evento")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataHora { get; set; }

        [Required(ErrorMessage = "A descrição do evento é obrigatória.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição do evento deve ter de 10 a 500 caracteres.")]
        [Display(Name = "Descrição do Evento")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Quantidade Máxima de Participantes")]
        [Range(1, 10000, ErrorMessage = "A quantidade máxima de participantes deve ser entre 1 e 10000.")]
        [DataType(DataType.Text)]
        public int QuantidadeMaximaParticipantes { get; set; }

        [Display(Name = "Quantidade de Participantes")]
        [Range(0, 10000, ErrorMessage = "A quantidade de participantes deve ser entre 0 e 10000.")]
        [DataType(DataType.Text)]
        public int QuantidadeParticipantes { get; set; }

        [Required(ErrorMessage = "O status do evento é obrigatório.")]
        [Display(Name = "Status do Evento")]
        [DataType(DataType.Text)]
        public bool Ativo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Participante> Participantes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Categoria> Categorias { get; set; }

        [JsonIgnore]
        public virtual ICollection<Organizador> Organizadores { get; set; }

        public Evento()
        {
            Ativo = true;
            QuantidadeParticipantes = 0;
            Participantes = new HashSet<Participante>();
            Categorias = new HashSet<Categoria>();
            Organizadores = new HashSet<Organizador>();
        }
    }
}