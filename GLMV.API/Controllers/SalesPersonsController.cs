using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMV.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesPersonsController : ControllerBase
    {
        private readonly SalesPersonService _salesPersonService;


        public SalesPersonsController(SalesPersonService salesPersonService)
        {
            _salesPersonService = salesPersonService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesPerson>>> GetSalesPersons()
        {
            return await _salesPersonService.GetAllAsync(); ;
        }


        [HttpGet("{id}")]
        public ActionResult<SalesPerson> GetSalesPerson(string id)
        {
            var salesPerson = _salesPersonService.GetByIdString(id);

            if (salesPerson == null)
            {
                return NotFound();
            }

            return Ok(salesPerson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesPerson(string id, SalesPersonViewModel model)
        {
            var salesPerson = _salesPersonService.GetByIdString(id);

            salesPerson.FirstName = model.FirstName;
            salesPerson.LastName = model.LastName;
            salesPerson.Email = model.Email;
            salesPerson.DataAtualizacao = DateOnly.FromDateTime(DateTime.UtcNow);
            
            _salesPersonService.Update(salesPerson);

            try
            {
                await _salesPersonService.SaveASync();

                return Ok(salesPerson);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesPersonExists(id))
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
        public async Task<ActionResult<SalesPerson>> PostSalesPerson(SalesPersonViewModel model)
        {
            var salesPerson = new SalesPerson();
            salesPerson.FirstName = model.FirstName;
            salesPerson.LastName = model.LastName;
            salesPerson.Email = model.Email;
            salesPerson.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
            salesPerson.DataAtualizacao = DateOnly.FromDateTime(DateTime.Now);

            await _salesPersonService.AddAsync(salesPerson);
            try
            {
                await _salesPersonService.SaveASync();
            }
            catch (DbUpdateException)
            {
                if (SalesPersonExists(salesPerson.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSalesPerson", new { id = salesPerson.Id }, salesPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesPerson(string id)
        {
            var salesPerson = _salesPersonService.GetByIdString(id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            var existProducts = _salesPersonService.isProductBySalesPerson(id);

            if (existProducts)
            {
                return Problem($"Existem produtos cadastrados para esse vendedor {salesPerson.FirstName}. Exclusão não é possível!");
            }

            await _salesPersonService.DeleteAsync(salesPerson);
            await _salesPersonService.SaveASync();

            return NoContent();
        }

        private bool SalesPersonExists(string id)
        {
            return _salesPersonService.isSalesPersonExists(id);
        }
    }
}
