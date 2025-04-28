using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GLMV.Domain.Models
{
    public class Category : Entity
    {

        [DisplayName("Nome")]
        [Required]
        [StringLength(100, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [Required]
        [StringLength(100, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 20)]
        public string Description { get; set; }

    }
}
