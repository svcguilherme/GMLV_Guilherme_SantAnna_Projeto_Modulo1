using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SALESPERSON")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _serviceCategory;

        public CategoriesController(CategoryService serviceCategory)
        {
            _serviceCategory = serviceCategory;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _serviceCategory.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = _serviceCategory.GetById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryViewModel model)
        {

            var category = _serviceCategory.GetById(id);

            category.Description = model.Description;
            category.Name = model.Name;
            category.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

            _serviceCategory.Update(category);

            try
            {
                await _serviceCategory.SaveASync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        public async Task<ActionResult<Category>> PostCategory(CategoryViewModel model)
        {
            var category = new Category();

            category.Description = model.Description;
            category.Name = model.Name;
            category.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            category.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

            await _serviceCategory.AddAsync(category);
            await _serviceCategory.SaveASync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = _serviceCategory.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            var isCategoryExist = _serviceCategory.isCategoryContainProducts(id);

            if (isCategoryExist)
            {
                return Problem($"Exclusão {category.Description} não é possível! Existem produtos vinculados categoria");
            }

            await _serviceCategory.DeleteAsync(category);
            await _serviceCategory.SaveASync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _serviceCategory.GetById(id) != null ? true : false;
        }
    }
}
