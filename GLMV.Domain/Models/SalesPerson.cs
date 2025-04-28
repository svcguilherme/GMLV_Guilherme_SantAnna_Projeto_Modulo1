using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GLMV.Domain.Models
{
    public class SalesPerson
    {
        [Key]
        public string Id { get; set; }

        [DisplayName("Nome")]
        [RequiredMessage]
        [StringLength(250, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string FirstName { set; get; }

        [DisplayName("Ultimo Nome")]
        [RequiredMessage]
        [StringLength(250, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string LastName { get; set; }

        [DisplayName("E-mail")]
        [RequiredMessage]
        [StringLength(250, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 20)]
        public string Email { get; set; }
        public List<Product> Products { get; set; }

        [Editable(false)]
        [Required(ErrorMessage = ("O campo {0} é obrigatório"))]
        [DataType(DataType.Date, ErrorMessage = "O Campo {0} está em formato inválido")]
        public DateOnly DataCadastro { get; set; } = default(DateOnly);

        [Editable(false)]
        [Required(ErrorMessage = ("O campo {0} é obrigatório"))]
        [DataType(DataType.Date, ErrorMessage = "O Campo {0} está em formato inválido")]
        public DateOnly DataAtualizacao { get; set; } = default(DateOnly);

    }
}
