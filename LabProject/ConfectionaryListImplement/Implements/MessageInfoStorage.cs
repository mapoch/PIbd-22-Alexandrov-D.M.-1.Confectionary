using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryListImplement.Models;

namespace ConfectionaryListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;

        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfos) result.Add(new MessageInfoViewModel
            {
                MessageId = messageInfo.MessageId,
                SenderName = messageInfo.SenderName,
                DateDelivery = messageInfo.DateDelivery,
                Subject = messageInfo.Subject,
                Body = messageInfo.Body
            });
            return result;
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessageInfos)
            {
                if ((model.ClientId.HasValue && messageInfo.ClientId == model.ClientId) ||
                        (!model.ClientId.HasValue && messageInfo.DateDelivery.Date == model.DateDelivery.Date))
                result.Add(new MessageInfoViewModel
                {
                    MessageId = messageInfo.MessageId,
                    SenderName = messageInfo.SenderName,
                    DateDelivery = messageInfo.DateDelivery,
                    Subject = messageInfo.Subject,
                    Body = messageInfo.Body
                });
            }
            return result;
        }
        public void Insert(MessageInfoBindingModel model)
        {
            source.MessageInfos.Add(new MessageInfo
                {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.FromMailAddress,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body
            });
        }
    }
}
