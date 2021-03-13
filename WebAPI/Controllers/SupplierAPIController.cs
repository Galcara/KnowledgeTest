using BusinessLogicalLayer.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupplierAPIController : ControllerBase
    {
        private readonly ISupplierService _SupplierService;

        public SupplierAPIController(ISupplierService supplierService)
        {
            this._SupplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supplier supplier)
        {
            Response response = await _SupplierService.Insert(supplier);
            return Ok(response);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update (Supplier supplier)
        {
            Response response = await _SupplierService.Update(supplier);
            if (!response.Success)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [Route("GetActives")]
        [HttpGet]
        public async Task<IActionResult> GetAllActives()
        {
            QueryResponse<Supplier> response = await _SupplierService.GetAll();
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Route("ByName")]
        [HttpGet]
        public async Task<IActionResult> GetAllByName(string name)
        {
            QueryResponse<Supplier> response = await _SupplierService.GetByName(name);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Route("CpfOrCnpj")]
        [HttpGet]
        public async Task<IActionResult> GetAllByCpfCnpj(string cpfCnpj)
        {
            QueryResponse<Supplier> response = await _SupplierService.GetByCpfOrCnpj(cpfCnpj);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Route("Register")]
        [HttpGet]
        public async Task<IActionResult> GetAllByRegister(DateTime registerDate)
        {
            QueryResponse<Supplier> response = await _SupplierService.GetByRegisterDate(registerDate);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [Route("Detail")]
        [HttpGet]
        public async Task<IActionResult> GetByID(int id)
        {
            SingleResponse<Supplier> response = await _SupplierService.GetByID(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Response response = await _SupplierService.Delete(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
