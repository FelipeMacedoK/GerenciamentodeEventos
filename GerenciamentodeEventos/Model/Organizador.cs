using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GerenciamentodeEventos.Model
{
    public class Organizador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Organizador")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do organizador é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome do organizador deve ter de 3 a 50 caracteres.")]
        [Display(Name = "Nome do Organizador")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do organizador é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O email do organizador deve ter de 5 a 100 caracteres.")]
        [Display(Name = "Email do Organizador")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone do organizador é obrigatório.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone do organizador deve ter de 10 a 15 caracteres.")]
        [Display(Name = "Telefone do Organizador")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A senha do organizador é obrigatória.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha do organizador deve ter de 6 a 20 caracteres.")]
        [Display(Name = "Senha do Organizador")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "O status do organizador é obrigatório.")]
        [Display(Name = "Status do Organizador")]
        [DataType(DataType.Text)]
        public bool Ativo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }

        public Organizador()
        {
            Ativo = true;
            Eventos = new HashSet<Evento>();
        }
    }
}