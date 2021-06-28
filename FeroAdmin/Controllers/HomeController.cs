using FeroAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FeroAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            int a = await StaffLogin(model);
            if(a == 0)
                return RedirectToAction("Index");
            else
            {
                ViewBag.error = "Invalid username or password";
                return View();
            }
        }

        private async Task<int> StaffLogin(LoginModel model)
        {
            return 0;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetModel();
            ViewBag.data = model;
            return View();
        }

        private async Task<List<Model>> GetModel()
        {
            using (var client = new HttpClient())
            {
                List<Model> models = new List<Model>();
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("models");
                if(res.IsSuccessStatusCode)
                {
                    var modelres = res.Content.ReadAsStringAsync().Result;
                    models = JsonConvert.DeserializeObject<List<Model>>(modelres);
                }
                return models;
            }
        }

        [HttpPost, ActionName("Inactive")]
        public async Task<IActionResult> Inactive(ChangeStatusModel model)
        {
            await ChangeConnect(model.ModelId);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Active")]
        public async Task<IActionResult> Active(ChangeStatusModel model)
        {
            await ChangeConnect(model.ModelId);
            return RedirectToAction("Index");
        }

        private async Task<string> ChangeConnect(string modelId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/v1/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.PutAsync("models/" + modelId +"/status", null);
                if (res.IsSuccessStatusCode)
                {
                    return "Update success";
                }
                return "Update faild";
            }
        }
    }
}
