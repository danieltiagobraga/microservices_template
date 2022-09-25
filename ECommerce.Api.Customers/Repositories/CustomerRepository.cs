using ECommerce.Api.Customers.Db;

namespace ECommerce.Api.Customers.Repositories
{
    public class CustomerRepository : Repository<Db.Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomersDbContext context) : base(context)
        {
        }
    }
}
