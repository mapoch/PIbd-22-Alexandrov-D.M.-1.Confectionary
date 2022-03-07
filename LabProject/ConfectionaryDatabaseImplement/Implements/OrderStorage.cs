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
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new ConfectionaryDatabase();
            return context.Orders.Select(CreateModel).ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            return context.Orders.Where(rec => rec.PastryId == model.PastryId)
                .Select(CreateModel).ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new ConfectionaryDatabase();
            var order = context.Orders.
                FirstOrDefault(rec => rec.PastryId == model.PastryId || rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
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
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(OrderBindingModel model)
        {
            using var context = new ConfectionaryDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        private static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.PastryId = model.PastryId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private static OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                PastryId = order.PastryId,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
