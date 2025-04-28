using System.ComponentModel.DataAnnotations;

namespace GLMV.Domain.Models
{
    public class Entity
    {
        public Entity()
        {
            DataCadastro = new DateOnly();
            DataAtualizacao = new DateOnly();
        }

        [Key]
        public int Id { get; set; }

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
