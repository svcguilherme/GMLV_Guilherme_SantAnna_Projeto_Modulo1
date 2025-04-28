using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLMV.Domain.Models
{
    public class SalesPersonViewModel
    {
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

    }
}
