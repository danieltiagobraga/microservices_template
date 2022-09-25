
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Repositories;

namespace ECommerce.Api.Orders.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(ProductsDbContext context) : base(context)
        {
        }
    }
}
