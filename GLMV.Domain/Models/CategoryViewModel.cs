using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLMV.Domain.Models
{
    public class CategoryViewModel
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
