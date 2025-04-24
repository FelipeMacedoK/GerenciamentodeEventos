using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("categoria")]
    public class Categoria
    {
        [Column("idcategoria")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID da Categoria")]
        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        public int IdCategoria { get; set; }

        [Column("nome")]
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter de 3 a 100 caracteres.")]
        [Display(Name = "Nome da Categoria")]
        [DataType(DataType.Text)]
        public string Nome { get; set; } = string.Empty;

        [Column("descricao")]
        [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição da categoria deve ter de 10 a 500 caracteres.")]
        [Display(Name = "Descrição da Categoria")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; } = string.Empty;

    }
}
