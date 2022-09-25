using ECommerce.Api.Orders.Db;

namespace ECommerce.Api.Orders.Repositories
{
    public class OrderRepository : Repository<Db.Order>, IOrderRepository
    {
        public OrderRepository(OrdersDbContext context) : base(context)
        {
        }
    }
}
