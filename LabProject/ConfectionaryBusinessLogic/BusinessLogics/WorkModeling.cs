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
            ConcurrentBag<OrderViewModel> ordersNew = new(orderLogic.Read(new OrderBindingModel
            { SearchStatus = OrderStatus.Принят }));
            ConcurrentBag<OrderViewModel> ordersOld = new(orderLogic.Read(new OrderBindingModel
            { SearchStatus = OrderStatus.Требуются_материалы }));
            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, ordersNew, ordersOld));
            }
        }

        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> ordersNew, ConcurrentBag<OrderViewModel> ordersOld)
        {
            var runOrders = await Task.Run(() => orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
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
            await Task.Run(() =>
            {
                while (!ordersOld.IsEmpty)
                {
                    if (ordersOld.TryTake(out OrderViewModel order))
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
                while (!ordersNew.IsEmpty)
                {
                    if (ordersNew.TryTake(out OrderViewModel order))
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
