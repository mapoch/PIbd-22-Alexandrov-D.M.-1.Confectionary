using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryDatabaseImplement.Models;

namespace ConfectionaryDatabaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using var context = new ConfectionaryDatabase();
            return context.Clients.Select(CreateModel).ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            return context.Clients.Where(rec => rec.FIO.Contains(model.FIO))
                .Select(CreateModel).ToList();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            var client = context.Clients.
                FirstOrDefault(rec => rec.FIO == model.FIO || rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            context.Clients.Add(CreateModel(model, new Client()));
            context.SaveChanges();
        }

        public void Update(ClientBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(ClientBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Client CreateModel(ClientBindingModel model, Client client)
        {
            client.FIO = model.FIO;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }

        private static ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel { Id = client.Id, FIO = client.FIO, Login = client.Login, Password = client.Password };
        }
    }
}
