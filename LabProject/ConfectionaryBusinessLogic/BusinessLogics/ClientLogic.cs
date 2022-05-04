using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class ClientLogic : IClientLogic
    {
        private readonly IClientStorage clientStorage;

        private readonly int passwordMaxLength = 50;
        private readonly int passwordMinLength = 10;

        public ClientLogic(IClientStorage _clientStorage)
        {
            clientStorage = _clientStorage;
        }

        public List<ClientViewModel> Read(ClientBindingModel model)
        {
            if (model == null)
            {
                return clientStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ClientViewModel> { clientStorage.GetElement(model) };
            }
            return clientStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ClientBindingModel model)
        {
            var element = clientStorage.GetElement(new ClientBindingModel { Login = model.Login });

            if (element != null && element.Id != model.Id) throw new Exception("Уже есть клиент с таким логином");

            if (!Regex.IsMatch(model.Login, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("В качестве логина должна быть указана почта");
            }
            if (model.Password.Length > passwordMaxLength || model.Password.Length < passwordMinLength 
                || !Regex.IsMatch(model.Password, @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {passwordMinLength} до { passwordMaxLength } " +
                    $"должен состоять и из цифр, букв и небуквенных символов");
            }

            if (model.Id.HasValue)
            {
                clientStorage.Update(model);
            }
            else
            {
                clientStorage.Insert(model);
            }
        }

        public void Delete(ClientBindingModel model)
        {
            var element = clientStorage.GetElement(new ClientBindingModel { Id = model.Id });

            if (element == null) throw new Exception("Элемент не найден");

            clientStorage.Delete(model);
        }
    }
}
