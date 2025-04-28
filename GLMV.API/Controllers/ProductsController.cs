using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMV.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IList<Product>> GetProducts(string SalesPersonId)
        {
            var products = _productService.GetAllProductsBySalesPerson(SalesPersonId);

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProduct(int id, ProductsViewModel model)
        {
       
            var product = _productService.GetById(id);

            product.Title = model.Title;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            product.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

            _productService.Update(product);

            try
            {
                await _productService.SaveASync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> PostProduct(ProductsViewModel model)
        {
            Product product = new Product();

            product.Title = model.Title;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;
            product.Quantity = model.Quantity;
            product.SalesPersonId = model.SalesPersonId;
            product.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            product.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);
          
            await _productService.AddAsync(product);
            await _productService.SaveASync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =  _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(product);
            await _productService.SaveASync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _productService.GetById(id) != null ? true : false;
        }
    }
}
