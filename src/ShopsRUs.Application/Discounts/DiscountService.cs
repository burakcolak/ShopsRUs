using Microsoft.Extensions.Options;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Discounts;

public class DiscountService : IDiscountService
{
    private readonly DiscountSettings _discountSettings;

    public DiscountService(IOptions<DiscountSettings> discountSettings)
    {
        _discountSettings = discountSettings.Value;
    }

    public decimal GetPercentageDiscountedAmount(Product product, Customer customer)
    {
        //Employee discount
        if (customer.IsEmployee)
        {
            return CalculatePercentageDiscountedAmount(product, _discountSettings.EmployeeDiscountPercent);
        }

        //Affiliate discount
        if (customer.IsAffiliate)
        {
            return CalculatePercentageDiscountedAmount(product, _discountSettings.AffiliateDiscountPercent);
        }

        //Customer loyalty discount (if customer for over 2 years)
        int customerAge = (DateTime.Now - customer.CreatedAt).Days;
        if (customerAge >= 365 * _discountSettings.DiscountEligibilityYears)
        {
            return CalculatePercentageDiscountedAmount(product, _discountSettings.CustomerLoyaltyDiscount);
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
        decimal discount = Math.Floor(totalAmount / _discountSettings.TotalAmountForDiscount) * _discountSettings.DiscountAmount;

        return totalAmount - discount;
    }
}
