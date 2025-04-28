using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GLMV.AppWeb.Controllers
{

    //   [Authorize(Roles = "SALESPERSON")]  - bão consegui colocar o authorize para funcionar
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ProductsController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetProductsWithCategories();
            return View(products);
        }

        [HttpGet("create-product/{id}")]
        public async Task<IActionResult> Create(string id)
        {
            if (!_categoryService.isCategoryExists())
            {
                TempData["StatusCategoriaErro"] = "Sem Categoria. Adicione uma categoria!";
                return RedirectToAction("Index", "GestaoLoja", new { id });
            }

            var categories = await _categoryService.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Description");

            var product = new Product
            {
                SalesPersonId = id,
                DataAtualizacao = DateOnly.FromDateTime(DateTime.Now),
                DataCadastro = DateOnly.FromDateTime(DateTime.Now)
            };

            return View(product);
        }

        [HttpPost("create-product")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("Title,Description,Price,Quantity,CategoryId,SalesPersonId")] Product product, IFormFile ImageUrl)
        {
            var categories = await _categoryService.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Description", product.CategoryId);

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageUrl != null && ImageUrl.Length > 0)
                    {
                        var nameFile = await UploadImageAsync(ImageUrl);
                        product.ImageUrl = nameFile;
                    }

                    product.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
                    product.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

                    await _productService.AddAsync(product);
                    await _productService.SaveASync();

                    TempData["SuccessMessage"] = "Produto cadastrado com sucesso!";
                    return RedirectToAction("Index", "GestaoLoja", new { id = product.SalesPersonId });
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Erro ao cadastrar produto: {ex.Message}";
                    return RedirectToAction("Create", "Products", new { id = product.SalesPersonId });
                }
            }

            TempData["ErrorMessage"] = "Corrija os erros no formulário antes de continuar.";
            return View("Create", product);
        }

        [HttpGet("detail-product/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetAllProductsWithCategoriesByIdAsync(id);
            return product != null ? View(product) : NotFound();
        }

        [HttpGet("edit-product/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = _productService.GetById(id);

            if (product == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Description", product.CategoryId);
            return View(product);
        }

        [HttpPost("edit-product")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Price,Quantity,CategoryId,ImageUrl,Id,SalesPersonId")] Product product, IFormFile ImageUrl)
        {
            if (id != product.Id) return NotFound();

            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Description", product.CategoryId);

            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        if (ImageUrl != null && ImageUrl.Length > 0)
                        {
                            var nameFile = await UploadImageAsync(ImageUrl);
                            product.ImageUrl = nameFile;
                        }

                        product.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

                        _productService.Update(product);
                        await _productService.SaveASync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await ProductExistsAsync(product.Id))
                            return NotFound();
                        else
                            throw;
                    }

                    return RedirectToAction("Index", "GestaoLoja", new { id = product.SalesPersonId });
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Erro ao cadastrar produto: {ex.Message}";
                    return RedirectToAction("Create", "Products", new { id = product.SalesPersonId });
                }
            }

            TempData["ErrorMessage"] = "Corrija os erros no formulário antes de continuar.";
            return View("Create", product);

        }

        [HttpGet("delete-product/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetAllProductsWithCategoriesByIdAsync(id);
            return product != null ? View(product) : NotFound();
        }

        [HttpPost("delete-product")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                await _productService.DeleteAsync(product);
                await _productService.SaveASync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExistsAsync(int id)
        {
            return _productService.isProductExistsAsync(id);
        }

        public async Task<string> UploadImageAsync(IFormFile ImageUpload)
        {
            if (ImageUpload == null || ImageUpload.Length == 0)
                return "images/products/notfound.jpg";

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(ImageUpload.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ImageUpload.CopyToAsync(stream);
            }

            return "/images/products/" + fileName;
        }
    }
}
