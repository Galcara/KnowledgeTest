using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PresentationLayerMVC.Models;
using PresentationLayerMVC.Models.CompanyModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayerMVC.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IMapper _mapper;

        public CompanyController(IMapper mapper)
        {
            this._mapper = mapper;

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyInsertViewModel viewModel)
        {
            Company company = _mapper.Map<Company>(viewModel);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(Startup.UrlBase + "CompanyAPI", content);
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

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyUpdateViewModel viewModel)
        {
            Company company = _mapper.Map<Company>(viewModel);

            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.PostAsync(Startup.UrlBase + "CompanyAPI/Update", content);
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

        public async Task<IActionResult> Edit(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "CompanyAPI/Detail/?id=" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string companyJsonResponse = await response.Content.ReadAsStringAsync();
                    SingleResponse<Company> companyResponse = JsonConvert.DeserializeObject<SingleResponse<Company>>(companyJsonResponse);
                    CompanyQueryViewModel viewModel = _mapper.Map<CompanyQueryViewModel>(companyResponse.Data);
                    return View(_mapper.Map<CompanyUpdateViewModel>(viewModel));
                }
                ViewBag.Errors = "Dados não encontrados.";
                return View();
            }
        }

        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "CompanyAPI/GetActives");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string companyJsonResponse = await response.Content.ReadAsStringAsync();
                    QueryResponse<Company> companyResponse = JsonConvert.DeserializeObject<QueryResponse<Company>>(companyJsonResponse);
                    return View(_mapper.Map<List<CompanyQueryViewModel>>(companyResponse.Data));
                }
                ViewBag.Errors = "Dados não encontrados.";
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Startup.UrlBase + "CompanyAPI/Detail/?id=" + id);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string companyJsonResponse = await response.Content.ReadAsStringAsync();
                    SingleResponse<Company> companyResponse = JsonConvert.DeserializeObject<SingleResponse<Company>>(companyJsonResponse);
                    return View(_mapper.Map<CompanyQueryViewModel>(companyResponse.Data));
                }
                ViewBag.Errors = "Dados não encontrados.";
                return View();
            }
        }

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
