using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GLMV.Domain.Models
{
    public class RegisterUserViewModel
    {

        [RequiredMessage]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Nome")]
        public string FirstName { get; set; }

        [RequiredMessage]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DisplayName("Ultimo Nome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [RequiredMessage]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        [DisplayName("Confirmar Password")]
        public string ConfirmPassword { get; set; }

    }
}
