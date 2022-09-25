using AutoMapper;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly IProductsRepository productsRepository;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(IProductsRepository productsRepository, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.logger = logger;
            this.mapper = mapper;
        }


        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = (await productsRepository.GetAllAsync()).FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Models.Product>(product);
                    return (true, result, String.Empty);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await productsRepository.GetAllAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Models.Product>>(products);
                    return (true, result, String.Empty);
                }

                return (false, new List<Models.Product>(), "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, new List<Models.Product>(), ex.Message);
            }
        }
    }
}
