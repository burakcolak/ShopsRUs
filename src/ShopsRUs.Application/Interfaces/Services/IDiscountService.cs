using ShopsRUs.Domain;

namespace ShopsRUs.Application.Interfaces.Services;

public interface IDiscountService
{
    decimal GetDiscountedAmount(Product product, Customer customer);
}
