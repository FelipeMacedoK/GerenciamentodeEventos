using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    public class Participante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Participante")]
        public int Id { get; set; }

        [ForeignKey("Evento")]
        [Display(Name = "ID do Evento")]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "O nome do participante é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do participante deve ter de 3 a 50 caracteres.")]
        [Display(Name = "Nome do Participante")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do participante é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O email do participante deve ter de 5 a 100 caracteres.")]
        [Display(Name = "Email do Participante")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone do participante é obrigatório.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone do participante deve ter de 10 a 15 caracteres.")]
        [Display(Name = "Telefone do Participante")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        public virtual Evento Evento { get; set; }

        public Participante()
        {
        }
    }
}