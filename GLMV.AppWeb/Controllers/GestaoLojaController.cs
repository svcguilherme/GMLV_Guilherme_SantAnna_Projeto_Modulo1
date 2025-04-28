using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GLMV.AppWeb.Controllers
{

    [Authorize]
    public class GestaoLojaController : Controller
    {
        private readonly ProductService _productService;
        private readonly SalesPersonService _salesPersonService;
        private readonly CategoryService _categoriaService;

        public GestaoLojaController(SalesPersonService salesPersonService, ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _salesPersonService = salesPersonService;
            _categoriaService = categoryService;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var listCategories = _categoriaService.GetAllAsync();

            var model = _salesPersonService.GetSalesPersonProduct(id);

            if (model == null)
                return NotFound();


            return View(model);
        }

        [HttpGet]
        public async Task<PartialViewResult> GetProductsBySalesPerson(string id)
        {
            var salesPerson = _salesPersonService.GetSalesPersonProduct(id);

            if (salesPerson == null)
                return PartialView("list-products", new List<Product>());


            return PartialView("list-products", salesPerson.Products);
        }

    }
}
