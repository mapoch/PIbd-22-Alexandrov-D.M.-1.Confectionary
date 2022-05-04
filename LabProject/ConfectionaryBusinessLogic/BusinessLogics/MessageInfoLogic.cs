using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.ViewModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.BusinessLogicContracts;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly IMessageInfoStorage messageInfoStorage;
        public MessageInfoLogic(IMessageInfoStorage _messageInfoStorage)
        {
            messageInfoStorage = _messageInfoStorage;
        }
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return messageInfoStorage.GetFullList();
            }
            return messageInfoStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(MessageInfoBindingModel model)
        {
            messageInfoStorage.Insert(model);
        }
    }
}
