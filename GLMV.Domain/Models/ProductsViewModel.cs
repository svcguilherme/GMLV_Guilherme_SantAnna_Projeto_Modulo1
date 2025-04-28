using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLMV.Domain.Models
{
    public class ProductsViewModel
        {
            [DisplayName("Título")]
            [StringLength(100, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 20)]
            [RequiredMessage]
            public string Title { get; set; }

            [DisplayName("Descrição")]
            [StringLength(250, ErrorMessage = "O campo precisar ter entre {2} e {1} caracteres.", MinimumLength = 20)]
            [RequiredMessage]
            public string Description { get; set; }

            [DisplayName("Valor R$")]
            [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser pelo menos 0,01.")]
            public decimal Price { get; set; }

            [DisplayName("Quantidade")]
            [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
            public int Quantity { get; set; }

            [DisplayName("Categoria")]
            public int CategoryId { get; set; }
    
            [DisplayName("Imagem do Produto")]
            public string? ImageUrl { get; set; }
            public string SalesPersonId { get; set; }
    }
}
