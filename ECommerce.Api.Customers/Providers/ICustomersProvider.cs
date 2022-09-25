using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Providers
{
    public interface ICustomersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Customer? Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
