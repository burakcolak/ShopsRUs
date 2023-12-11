using ShopsRUs.Domain;

namespace ShopsRUs.Application.Interfaces.Services;

public interface IDiscountService
{
    decimal GetPercentageDiscountedAmount(Product product, Customer customer);
    decimal CalculateDiscountedTotalAmount(decimal totalAmount);
}
