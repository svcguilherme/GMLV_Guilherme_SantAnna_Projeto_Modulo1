using GLMV.Application.Services;
using GLMV.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GLMV.AppWeb.Controllers
{

    //   [Authorize(Roles = "SALESPERSON")]  - bão consegui colocar o authorize para funcionar
    public class SalesPersonsController : Controller
    {
        private readonly SalesPersonService _service;

        public SalesPersonsController(SalesPersonService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = _service.GetByIdString(id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            return View(salesPerson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,Email")] SalesPerson salesPerson)
        {
            if (id != salesPerson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(salesPerson);
                    await _service.SaveASync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonExists(salesPerson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "GestaoLoja", new { Id = salesPerson.Id });
            }
            return RedirectToAction("Index", "GestaoLoja", new { Id = salesPerson.Id });

        }

        private bool SalesPersonExists(string id)
        {
            return _service.isSalesPersonExists(id);
        }
    }
}
