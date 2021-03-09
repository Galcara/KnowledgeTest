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
    public class CompanyAPIController : ControllerBase
    {
        private readonly ICompanyService _CompanyService;

        public CompanyAPIController(ICompanyService companyService)
        {
            this._CompanyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            Response response = await _CompanyService.Insert(company);
            return Ok(response);
        }

        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> Update (Company company)
        {
            Response response = await _CompanyService.Update(company);
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
            Response response = await _CompanyService.GetAll();
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
            SingleResponse<Company> response = await _CompanyService.GetByID(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Response response = await _CompanyService.Delete(id);
            if (!response.Success)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
