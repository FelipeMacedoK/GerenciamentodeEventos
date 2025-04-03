using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GerenciamentodeEventos.Model
{
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID da Categoria")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter de 3 a 50 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "A descrição da categoria deve ter de 10 a 200 caracteres.")]
        [Display(Name = "Descrição da Categoria")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Status da Categoria")]
        public bool Ativo { get; set; }

        public virtual ICollection<Evento> Eventos { get; set; }

        public Categoria()
        {
            Ativo = true;
            Eventos = new HashSet<Evento>();
        }
    }
}