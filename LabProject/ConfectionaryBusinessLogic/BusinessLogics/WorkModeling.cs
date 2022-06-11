using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfectionaryContracts.BindingModels;
using ConfectionaryContracts.BusinessLogicContracts;
using ConfectionaryContracts.StoragesContracts;
using ConfectionaryContracts.ViewModels;
using ConfectionaryContracts.Enums;

namespace ConfectionaryBusinessLogic.BusinessLogics
{
    public class WorkModeling : IWorkProcess
    {
        private IOrderLogic orderLogic;
        private readonly Random rnd;

        public WorkModeling()
        {
            rnd = new Random(1000);
        }

        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic _orderLogic)
        {
            orderLogic = _orderLogic;
            var implementers = implementerLogic.Read(null);
            ConcurrentBag<OrderViewModel> orders = new(orderLogic.Read(new OrderBindingModel
            { SearchStatus = OrderStatus.Принят }));
            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, orders));
            }
        }

        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> newOrders)
        {
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));

            var stoppedOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Требуются_материалы
            }));

            foreach (var order in runOrders)
            {
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count * 1000);
                orderLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id
                });
                Thread.Sleep(implementer.PauseTime * 1000);
            }
            foreach (var order in stoppedOrders)
            {
                if (orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                { OrderId = order.Id, ImplementerId = implementer.Id }))
                {
                    Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count * 1000);
                    orderLogic.FinishOrder(new ChangeStatusBindingModel
                    { OrderId = order.Id });
                    Thread.Sleep(implementer.PauseTime * 1000);
                }
            }
            await Task.Run(() =>
            {
                while (!newOrders.IsEmpty)
                {
                    if (newOrders.TryTake(out OrderViewModel order))
                    {
                        if (orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                        { OrderId = order.Id, ImplementerId = implementer.Id }))
                        {
                            Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count * 1000);
                            orderLogic.FinishOrder(new ChangeStatusBindingModel
                            { OrderId = order.Id });
                            Thread.Sleep(implementer.PauseTime * 1000);
                        }
                    }
                }
            });
        }
    }
}
