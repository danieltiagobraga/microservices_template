using AutoMapper;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Repositories;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderItensRepository orderItensRepository;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(IOrderRepository orderRepository, IOrderItensRepository orderItensRepository, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.orderItensRepository = orderItensRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = (await orderRepository.GetAllAsync()).Where(e => e.CustomerId == customerId);
                if (orders != null && orders.Any())
                {
                    var ordersDTOs = mapper.Map<IEnumerable<Db.Order>, 
                        IEnumerable<Models.Order>>(orders);

                    foreach (var order in ordersDTOs)
                    {
                        var orderItens = orderItensRepository.Find(e => e.OrderId == order.Id);
                        var orderItensDTO = mapper.Map<IEnumerable<Db.OrderItem>, IEnumerable<Models.OrderItem>>(orderItens);

                        order.Items = orderItensDTO.ToList();
                    }


                    return (true, ordersDTOs, String.Empty);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }    
        }
    }
}