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
        private readonly IClientLogic logicC;
        private readonly IMessageInfoLogic logicMI;

        private readonly ILogger<MainController> logger;

        public ClientController(IClientLogic _logicC, IMessageInfoLogic _logicMI)
        {
            logicC = _logicC;
            logicMI = _logicMI;
        }

        [HttpGet]
        public ClientViewModel Login(string login, string password)
        {
            var list = logicC.Read(new ClientBindingModel { Login = login, Password = password });
            return (list != null && list.Count > 0) ? list[0] : null;
        }

        [HttpGet]
        public List<MessageInfoViewModel> GetMessageInfos(int clientId) => 
            logicMI.Read(new MessageInfoBindingModel { ClientId = clientId });

        [HttpPost]
        public void Register(ClientBindingModel model) => logicC.CreateOrUpdate(model);

        [HttpPost]
        public void UpdateData(ClientBindingModel model) => logicC.CreateOrUpdate(model);
    }
}
