using ECommerce.Api.Orders.Db;

namespace ECommerce.Api.Orders.Repositories
{
    public class OrderItensRepository : Repository<Db.OrderItem>, IOrderItensRepository
    {
        public OrderItensRepository(OrdersDbContext context) : base(context)
        {
        }
    }
}
