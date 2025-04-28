using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GLMV.AppWeb.Controllers
{
    //   [Authorize(Roles = "SALESPERSON")]  - bão consegui colocar o authorize para funcionar
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoriaService;

        public CategoryController(CategoryService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriaService.GetAllAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriaService.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
                category.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

                _categoriaService.AddAsync(category);
                await _categoriaService.SaveASync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriaService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id,DataCadastro,DataAtualizacao")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);
                    _categoriaService.Update(category);
                    await _categoriaService.SaveASync();
                }
                catch
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoriaService.GetById(id);

            if (_categoriaService.isCategoryContainProducts(id))
            {
                TempData["StatusCategoriaErro"] = "Categoria Contem Produtos! Exclusão não é possível";

                return RedirectToAction(nameof(Index));
            }

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = _categoriaService.GetById(id);
            if (category != null)
            {
                _categoriaService.DeleteAsync(category);
            }

            await _categoriaService.SaveASync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists()
        {
            return _categoriaService.isCategoryExists();
        }
    }
}
