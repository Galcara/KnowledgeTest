using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PresentationLayerMVC.Models;
using PresentationLayerMVC.Models.CompanyModels;
using PresentationLayerMVC.Models.SupplierModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IMapper _mapper;


        public SupplierController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<IActionResult> CreateCPF()
        {
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage responseMessage = await client.GetAsync(Startup.UrlBase + "CompanyAPI/GetActives");
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                QueryResponse<Company> response = JsonConvert.DeserializeObject<QueryResponse<Company>>(jsonResponse);
                if (!response.Success)
                {
                    return RedirectToAction("Index", "Company");
                }
                ViewBag.Companies = response.Data.Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.CommercialName }).ToList();
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCPF(SupplierCpfInsertViewModel viewModel)
        {
            Supplier supplier = _mapper.Map<Supplier>(viewModel);
            viewModel.Companies.ForEach(c => supplier.Companies.Add(new Company() { ID = c }));

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(supplier), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(Startup.UrlBase + "SupplierAPI", content);
                string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                Response response = JsonConvert.DeserializeObject<Response>(jsonResponse);
                if (response.Success)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Erros = response.Message;
                return View();
            }
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "SupplierAPI/GetActives");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string supplierJsonResponse = await response.Content.ReadAsStringAsync();
                    QueryResponse<Supplier> supplierResponse = JsonConvert.DeserializeObject<QueryResponse<Supplier>>(supplierJsonResponse);
                    return View(_mapper.Map<List<SupplierQueryViewModel>>(supplierResponse.Data));
                }
                ViewBag.Errors = "Dados não encontrados.";
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "SupplierAPI/Detail/?id=" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string supplierJsonResponse = await response.Content.ReadAsStringAsync();
                    SingleResponse<Supplier> supplierResponse = JsonConvert.DeserializeObject<SingleResponse<Supplier>>(supplierJsonResponse);
                    return View(_mapper.Map<SupplierQueryViewModel>(supplierResponse.Data));
                }
                ViewBag.Errors = "Dados não encontrados.";
                return View();
            }
        }
        //-----------------------------------------------------
        //[HttpPost]
        //public async Task<IActionResult> Edit(CompanyUpdateViewModel viewModel)
        //{
        //    Company company = _mapper.Map<Company>(viewModel);
        //
        //    using (HttpClient client = new HttpClient())
        //    {
        //        StringContent content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");
        //        HttpResponseMessage responseMessage = await client.PostAsync(Startup.UrlBase + "CompanyAPI/Update", content);
        //        string jsonResponse = await responseMessage.Content.ReadAsStringAsync();
        //        Response response = JsonConvert.DeserializeObject<Response>(jsonResponse);
        //        if (response.Success)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.Erros = response.Message;
        //        return View();
        //    }
        //}
        //
        //public async Task<IActionResult> Edit(int id)
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "CompanyAPI/Detail/?id=" + id);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            string companyJsonResponse = await response.Content.ReadAsStringAsync();
        //            SingleResponse<Company> companyResponse = JsonConvert.DeserializeObject<SingleResponse<Company>>(companyJsonResponse);
        //            CompanyQueryViewModel viewModel = _mapper.Map<CompanyQueryViewModel>(companyResponse.Data);
        //            return View(_mapper.Map<CompanyUpdateViewModel>(viewModel));
        //        }
        //        ViewBag.Errors = "Dados não encontrados.";
        //        return View();
        //    }
        //}


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
