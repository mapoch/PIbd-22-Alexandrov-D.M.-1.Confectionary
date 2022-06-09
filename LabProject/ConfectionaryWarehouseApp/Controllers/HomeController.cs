using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;
using ConfectionaryWarehouseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConfectionaryWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouselist"));
        }

        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (!(password == _configuration["Password"]))
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Authorized = true;
                Response.Redirect("Index");
                return;
            }

            throw new Exception("Введите пароль");
        }

        public IActionResult CreateOrUpdate(int? id)
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Enter");
            }
            if (id == null)
            {
                return View();
            }
            var warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/warehouse/getwarehouse?warehouseId={id}");
            if (warehouse == null)
            {
                return View();
            }
            return View(warehouse);
        }
        [HttpPost]
        public void Create([Bind("Name, Manager")] WarehouseBindingModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Manager))
            {
                return;
            }
            model.StoredComponents = new Dictionary<int, (string, int)>();
            APIClient.PostRequest("api/warehouse/create", model);
            Response.Redirect("Index");
        }
        [HttpPost]
        public IActionResult Update(int id, [Bind("Id,Name,Manager")] WarehouseBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/warehouse/getwarehouse?warehouseId={id}");
            model.StoredComponents = warehouse.StoredComponents;

            APIClient.PostRequest("api/warehouse/update", model);
            return Redirect("~/Home/Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/warehouse/getwarehouse?warehouseId={id}");
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            APIClient.PostRequest("api/warehouse/delete", new WarehouseBindingModel { Id = id });
            return Redirect("~/Home/Index");
        }

        public IActionResult Replenish()
        {
            if (!Program.Authorized)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WarehouseViewModel>>($"api/warehouse/getwarehouselist");
            ViewBag.Components = APIClient.GetRequest<List<ComponentViewModel>>($"api/warehouse/getcomponentlist");

            return View();
        }

        [HttpPost]
        public IActionResult Replenish(int Warehouse, int Component, int Count)
        {
            if (Warehouse == 0 || Component == 0 || Count <= 0)
            {
                return NotFound();
            }

            var warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/warehouse/getwarehouse?warehouseId={Warehouse}");
            if (warehouse == null)
            {
                return NotFound();
            }

            var component = APIClient.GetRequest<ComponentViewModel>($"api/warehouse/getcomponent?componentId={Component}");
            if (component == null)
            {
                return NotFound();
            }

            APIClient.PostRequest($"api/warehouse/replenish", new ReplenishBindingModel { WarehouseId = Warehouse, ComponentId = Component, Count = Count});
            return Redirect("~/Home/Replenish");
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
