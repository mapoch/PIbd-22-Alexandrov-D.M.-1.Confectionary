using System;
using System.Collections.Generic;
using System.Text;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryContracts.Enums;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage orderStorage;
        private readonly IWarehouseStorage warehouseStorage;

        public OrderLogic(IOrderStorage _orderStorage, IWarehouseStorage _warehouseStorage)
        {
            orderStorage = _orderStorage;
            warehouseStorage = _warehouseStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { orderStorage.GetElement(model) };
            }
            return orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            OrderBindingModel order = new OrderBindingModel 
            { PastryId = model.PastryId, Count = model.Count, Sum = model.Sum, Status = 0, 
                DateCreate = DateTime.Now, ClientId = model.ClientId };

            orderStorage.Insert(order);
        }

        public bool TakeOrderInWork(ChangeStatusBindingModel model)
        {
            bool success = false;

            var element = orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });

            if (element == null) throw new Exception("Элемент не найден");

            if (!(element.Status.Contains(OrderStatus.Принят.ToString()) || element.Status.Contains(OrderStatus.Требуются_материалы.ToString()))) 
                throw new Exception("Не в статусе \"Принят\" или \"Требуются материалы\"");

            OrderBindingModel order = new OrderBindingModel
            {
                Id = model.OrderId,
                Status = OrderStatus.Требуются_материалы,
                PastryId = element.PastryId,
                Count = element.Count,
                Sum = element.Sum,
                DateCreate = element.DateCreate
            };

            if (warehouseStorage.IsEnough(order)) { 
                order.Status = OrderStatus.Выполняется;
                order.DateImplement = DateTime.Now;
                order.ImplementerId = model.ImplementerId;
                success = true;
            }

            orderStorage.Update(order);
            return success;
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var element = orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });

            if (element == null) throw new Exception("Элемент не найден");

            if (!element.Status.Contains(OrderStatus.Выполняется.ToString())) 
                throw new Exception("Не в статусе \"Выполняется\"");

            orderStorage.Update(new OrderBindingModel { Id = model.OrderId, Status = OrderStatus.Готов, DateImplement = element.DateImplement,
                PastryId = element.PastryId, Count = element.Count, Sum = element.Sum, DateCreate = element.DateCreate, ClientId = element.ClientId,
                ImplementerId = element.ImplementerId});
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var element = orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });

            if (element == null) throw new Exception("Элемент не найден");

            if (!element.Status.Contains(OrderStatus.Готов.ToString())) throw new Exception("Не в статусе \"Готов\"");

            orderStorage.Update(new OrderBindingModel { Id = model.OrderId, Status = OrderStatus.Выдан, DateImplement = element.DateImplement,
                PastryId = element.PastryId, Count = element.Count, Sum = element.Sum, DateCreate = element.DateCreate, ClientId = element.ClientId,
                ImplementerId = element.ImplementerId});
        }
    }
}
