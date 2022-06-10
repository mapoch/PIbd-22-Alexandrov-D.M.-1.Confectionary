using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryRestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseLogic warehouse;
        private readonly IComponentLogic component;

        private readonly ILogger<MainController> _logger;

        public WarehouseController(IWarehouseLogic _warehouse, IComponentLogic _component)
        {
            warehouse = _warehouse;
            component = _component;
        }

        [HttpGet]
        public List<ComponentViewModel> GetComponentList() => component.Read(null)?.ToList();

        [HttpGet]
        public ComponentViewModel GetComponent(int componentId) => component.Read(new ComponentBindingModel { Id = componentId })?[0];

        [HttpGet]
        public List<WarehouseViewModel> GetWarehouseList() => warehouse.Read(null)?.ToList();

        [HttpGet]
        public WarehouseViewModel GetWarehouse(int warehouseId) => warehouse.Read(new WarehouseBindingModel { Id = warehouseId })?[0];

        [HttpPost]
        public void Create(WarehouseBindingModel model) => warehouse.CreateOrUpdate(model);

        [HttpPost]
        public void Update(WarehouseBindingModel model) => warehouse.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(WarehouseBindingModel model) => warehouse.Delete(model);

        [HttpPost]
        public void Replenish(ReplenishBindingModel model) => warehouse.AddComponents(model);
    }
}
