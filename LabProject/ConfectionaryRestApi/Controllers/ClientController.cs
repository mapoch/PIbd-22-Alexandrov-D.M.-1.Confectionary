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
    public class ClientController : ControllerBase
    {
        private readonly IClientLogic logic;

        private readonly ILogger<MainController> _logger;

        public ClientController(IClientLogic _logic)
        {
            logic = _logic;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = logic.Read(new ClientBindingModel { Login = login, Password = password });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpPost]
        public void Register(ClientBindingModel model) => logic.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => logic.CreateOrUpdate(model);
    }
}
