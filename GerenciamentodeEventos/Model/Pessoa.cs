using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Column("idpessoa")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID da Pessoa")]
        public int IdPessoa { get; set; }

        [Column("cpf")]
        [Required(ErrorMessage = "O CPF da pessoa é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF da pessoa deve ter 11 caracteres.")]
        [Display(Name = "CPF da Pessoa")]
        [DataType(DataType.Text)]
        public string Cpf { get; set; } = string.Empty;

        [Column("nome")]
        [Required(ErrorMessage = "O nome da pessoa é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da pessoa deve ter de 3 a 100 caracteres.")]
        [Display(Name = "Nome da Pessoa")]
        [DataType(DataType.Text)]
        public string Nome { get; set; } = string.Empty;

        [Column("email")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O e-mail da pessoa deve ter de 5 a 100 caracteres.")]
        [Display(Name = "E-mail da Pessoa")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Column("telefone")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone da pessoa deve ter de 10 a 15 caracteres.")]
        [Display(Name = "Telefone da Pessoa")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; } = string.Empty;

    }
}
