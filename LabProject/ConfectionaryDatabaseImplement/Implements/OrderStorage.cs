using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace ConfectionaryDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new ConfectionaryDatabase();
            //foreach (var ord in context.Orders) Delete(new OrderBindingModel { Id = ord.Id});
            return context.Orders.Include(rec => rec.Pastry).Include(rec => rec.Client).Select(CreateModel).ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            return context.Orders.Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue
                && rec.DateCreate.Date == model.DateCreate.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate.Date >= model.DateFrom.Value.Date
                && rec.DateCreate.Date <= model.DateTo.Value.Date) ||
                (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                .Include(rec => rec.Pastry).Include(rec => rec.Client).Select(CreateModel).ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            var order = context.Orders.Include(rec => rec.Pastry).Include(rec => rec.Client).
                FirstOrDefault(rec => rec.PastryId == model.PastryId || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            Order order = new Order();
            CreateModel(model, order, context);
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element, context);
            context.SaveChanges();
        }

        public void Delete(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Order CreateModel(OrderBindingModel model, Order order, ConfectionaryDatabase context)
        {
            order.PastryId = model.PastryId;
            order.Pastry = context.Pastries.FirstOrDefault(rec => rec.Id == model.PastryId);
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.ClientId = model.ClientId;
            order.Client = context.Clients.FirstOrDefault(rec => rec.Id == model.ClientId);
            return order;
        }

        private static OrderViewModel CreateModel(Order order)
        {
            int? clientId = null;
            string clientFIO = null;

            if (order.Client != null)
            {
                clientId = order.ClientId;
                clientFIO = order.Client.FIO;
            }

            return new OrderViewModel
            {
                Id = order.Id,
                PastryId = order.PastryId,
                PastryName = order.Pastry.PastryName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                ClientId = clientId,
                ClientFIO = clientFIO
            };
        }
    }
}
