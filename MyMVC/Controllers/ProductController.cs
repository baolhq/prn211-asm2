using System.Net;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using MyMVC.Models;

namespace MyMVC.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private string _productApi;

        public ProductController()
        {
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
            _productApi = "https://localhost:5001/api/Product";
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_productApi);
            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var products = JsonSerializer.Deserialize<List<Product>>(data, options);
            return View(products);
        }

        [HttpGet("/Create")]
        public IActionResult Create() => View();


        [HttpPost("/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var data = JsonSerializer.Serialize(product);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(_productApi, content);

                if (response.StatusCode == HttpStatusCode.OK) return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet("/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"{_productApi}/{id}");
            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var product = JsonSerializer.Deserialize<Product>(data, options);
            return View(product);
        }

        [HttpPost("/Delete/{id}")]
        public async Task<IActionResult> Delete(int id, Product product)
        {
            if (product == null) return BadRequest();

            var response = await _client.DeleteAsync($"{_productApi}/{id}");
            if (response.StatusCode == HttpStatusCode.NoContent) return RedirectToAction("Index");

            return View(product);
        }

        [HttpGet("/Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _client.GetAsync($"{_productApi}/{id}");
            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var product = JsonSerializer.Deserialize<Product>(data, options);
            return View(product);
        }


        [HttpPost("/Update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                var data = JsonSerializer.Serialize(product);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync($"{_productApi}/{id}", content);

                if (response.StatusCode == HttpStatusCode.OK) return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}