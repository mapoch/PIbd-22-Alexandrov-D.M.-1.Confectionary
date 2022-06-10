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
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic order;
        private readonly IPastryLogic pastry;

        private readonly ILogger<MainController> _logger;

        public MainController(IOrderLogic _order, IPastryLogic _pastry)
        {
            order = _order;
            pastry = _pastry;
        }

        [HttpGet]
        public List<PastryViewModel> GetPastryList() => pastry.Read(null)?.ToList();

        [HttpGet]
        public PastryViewModel GetPastry(int pastryId) => pastry.Read(new PastryBindingModel { Id = pastryId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => order.CreateOrder(model);
    }
}
