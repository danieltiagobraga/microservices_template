using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(ICustomerRepository customerRepository, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customers = await customerRepository.GetAllAsync();
                if (customers != null && customers.Any())
                {
                    logger?.LogInformation($"{customers.Count} customer(s) found");
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, String.Empty);
                }

                return (false, new List<Models.Customer>(), "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, new List<Models.Customer>(), ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer? Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                logger?.LogInformation("Querying customers");
                var customer =  await customerRepository.SingleOrDefaultAsync(e => e.Id == id);
                if (customer != null)
                {
                    logger?.LogInformation("Customer found");
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
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
    }
}