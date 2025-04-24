using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciamentodeEventos.Model
{
    [Table("local")]
    public class Local
    {
        [Column("idlocal")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID do Local")]
        public int IdLocal { get; set; }

        [Column("logradouro")]
        [Required(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Logradouro")]
        [DataType(DataType.Text)]
        public string Logradouro { get; set; } = string.Empty;

        [Column("numero")]
        [Required(ErrorMessage = "O número é obrigatório.")]
        [StringLength(10)]
        [Display(Name = "Número")]
        [DataType(DataType.Text)]
        public string Numero { get; set; } = string.Empty;

        [Column("bairro")]
        [Required(ErrorMessage = "O bairro é obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Bairro")]
        [DataType(DataType.Text)]
        public string Bairro { get; set; } = string.Empty;

        [Column("cidade")]
        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [StringLength(50)]
        [Display(Name = "Cidade")]
        [DataType(DataType.Text)]
        public string Cidade { get; set; } = string.Empty;

        [Column("estado")]
        [Required(ErrorMessage = "O estado é obrigatório.")]
        [StringLength(2)]
        [Display(Name = "Estado")]
        [DataType(DataType.Text)]
        public string Estado { get; set; } = string.Empty;

    }
}
