using ShopsRUs.Application.Discounts;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Common.Enums;
using ShopsRUs.Domain;
using Xunit;

namespace ShopsRUs.Tests.DiscountTests;
public class DiscountServiceTests
{
    private readonly IDiscountService _discountService;

    public DiscountServiceTests()
    {
        _discountService = new DiscountService();
    }

    [Fact]
    public void GetPercentageDiscountedAmount_EmployeeDiscount_ReturnsEmployeeDiscountedAmount()
    {
        // Arrange
        var product = new Product { Name = "Product 1", Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { Name = "John Doe", IsEmployee = true, IsAffiliate = false, CreatedAt = DateTime.Now.AddYears(-1) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 70; // Employee discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_AffiliateDiscount_ReturnsAffiliateDiscountedAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { IsAffiliate = true };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 90; // Affiliate discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_CustomerLoyaltyDiscount_ReturnsLoyaltyDiscountedAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 95; // Loyalty discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_NoDiscount_ReturnsOriginalAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Grocery };
        var customer = new Customer();

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 100; // No discount applies for groceries
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_AffiliateAndEmployeeDiscount_ReturnsEmployeeDiscountedAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { IsEmployee = true, IsAffiliate = true };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 70; // Only employee discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_LoyaltyAndAffiliateDiscount_ReturnsAffiliateDiscountedAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { IsEmployee = false, IsAffiliate = true, CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 90; // Only affiliate discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_CustomerLoyaltyAndGrocery_ReturnsOriginalAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Grocery };
        var customer = new Customer { CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 100; // Loyalty discount doesn't apply to groceries
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void GetPercentageDiscountedAmount_EmployeeAndCustomerLoyaltyAndGrocery_ReturnsOriginalAmount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Grocery };
        var customer = new Customer { IsEmployee = true, CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 100; // Loyalty or employee discounts doesn't apply to groceries
        Assert.Equal(expectedDiscountedAmount, result);
    }


    [Fact]
    public void GetPercentageDiscountedAmount_EmployeeAndCustomerLoyaltyAndAffiliate_ReturnsEmployeeDiscount()
    {
        // Arrange
        var product = new Product { Price = 100, Category = ProductCategory.Other };
        var customer = new Customer { IsEmployee = true, IsAffiliate = true, CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _discountService.GetPercentageDiscountedAmount(product, customer);

        // Assert
        decimal expectedDiscountedAmount = 70; // Only employee discount applies
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void CalculateDiscountedTotalAmount_ReturnsOriginalAmount()
    {
        // Arrange
        decimal totalAmount = 95;

        // Act
        var result = _discountService.CalculateDiscountedTotalAmount(totalAmount);

        // Assert
        decimal expectedDiscountedAmount = 95; // No discount applies under $100
        Assert.Equal(expectedDiscountedAmount, result);
    }

    [Fact]
    public void CalculateDiscountedTotalAmount_CalculatesDiscountCorrectly()
    {
        // Arrange
        decimal totalAmount = 300;

        // Act
        var result = _discountService.CalculateDiscountedTotalAmount(totalAmount);

        // Assert
        decimal expectedDiscountedAmount = 285; // $5 discount for every $100
        Assert.Equal(expectedDiscountedAmount, result);
    }
}
