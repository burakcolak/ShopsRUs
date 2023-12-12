using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Discount;

public class DiscountService : IDiscountService
{
    public decimal GetPercentageDiscountedAmount(Product product, Customer customer)
    {
        //Employee discount
        if (customer.IsEmployee)
        {
            return CalculatePercentageDiscountedAmount(product, 30);
        }

        //Affiliate discount
        if (customer.IsAffiliate)
        {
            return CalculatePercentageDiscountedAmount(product, 10);
        }

        //Customer loyalty discount (if customer for over 2 years)
        int customerAge = (DateTime.Now - customer.CreatedAt).Days;
        if (customerAge >= 365 * 2)
        {
            return CalculatePercentageDiscountedAmount(product, 5);
        }

        return product.Price;
    }

    private decimal CalculatePercentageDiscountedAmount(Product product, decimal percentage)
    {
        //The percentage based discounts do not apply on groceries.
        if (product.Category == Common.Enums.ProductCategory.Grocery)
        {
            return product.Price;
        }

        decimal discountedAmountPercentage = 100 - percentage;
        return (product.Price * discountedAmountPercentage) / 100;
    }

    public decimal CalculateDiscountedTotalAmount(decimal totalAmount)
    {
        // Calculate $5 discount for every $100
        decimal discount = Math.Floor(totalAmount / 100) * 5;

        return totalAmount - discount;
    }
}
