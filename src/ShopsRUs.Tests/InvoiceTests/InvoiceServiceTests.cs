using Microsoft.Extensions.Options;
using Moq;
using ShopsRUs.Application.Discounts;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Application.Invoices;
using ShopsRUs.Common.Enums;
using ShopsRUs.Contracts.Public.Invoice;
using ShopsRUs.Domain;
using Xunit;

namespace ShopsRUs.Tests.InvoiceTests;
public class InvoiceServiceTests
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceServiceTests()
    {
        var mockOptions = new Mock<IOptions<DiscountSettings>>();
        var discountSettings = new DiscountSettings
        {
            EmployeeDiscountPercent = 30,
            AffiliateDiscountPercent = 10,
            CustomerLoyaltyDiscount = 5,
            DiscountEligibilityYears = 2,
            TotalAmountForDiscount = 100,
            DiscountAmount = 5
        };

        mockOptions.Setup(o => o.Value).Returns(discountSettings);
        _invoiceService = new InvoiceService(new DiscountService(mockOptions.Object));
    }

    [Fact]
    public void GenerateInvoice_WithAffiliateDiscountAndTotalAmountDiscount_ReturnsCorrectInvoice()
    {
        // Arrange
        var products = new List<ProductRequest>
        {
            new ProductRequest { Name = "Product 1", Price = 100, Category = ProductCategory.Other },
            new ProductRequest { Name = "Product 2", Price = 100, Category = ProductCategory.Other },
            new ProductRequest { Name = "Product 3", Price = 100, Category = ProductCategory.Other },
        };
        var customer = new CustomerRequest { Name = "John Doe", IsEmployee = false, IsAffiliate = true, CreatedAt = DateTime.Now.AddYears(-1) };

        // Act
        var result = _invoiceService.GenerateInvoice(products, customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Products.Count);

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);
        decimal percentageDiscountedAmount = 30; // 10% discount for affiliate
        decimal totalAmountDiscount = 10; // $5 discount for every $100
        decimal totalDiscount = percentageDiscountedAmount + totalAmountDiscount;
        decimal finalAmount = totalPriceBeforeDiscount - totalDiscount;

        Assert.Equal(totalPriceBeforeDiscount, result.TotalPriceBeforeDiscount);
        Assert.Equal(totalDiscount, result.TotalDiscount);
        Assert.Equal(finalAmount, result.FinalAmount);
    }

    [Fact]
    public void GenerateInvoice_WithEmployeeDiscountAndTotalAmountDiscount_ReturnsCorrectInvoice()
    {
        // Arrange
        var products = new List<ProductRequest>
        {
            new ProductRequest { Name = "Product 1", Price = 100, Category = ProductCategory.Other },
            new ProductRequest { Name = "Product 2", Price = 50, Category = ProductCategory.Other },
        };

        var customer = new CustomerRequest { IsEmployee = true };

        // Act
        var result = _invoiceService.GenerateInvoice(products, customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Products.Count);

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);
        decimal percentageDiscountedAmount = 45; // 30% discount for employee
        decimal totalAmountDiscount = 5; // $5 discount for every $100
        decimal totalDiscount = percentageDiscountedAmount + totalAmountDiscount;
        decimal finalAmount = totalPriceBeforeDiscount - totalDiscount;

        Assert.Equal(totalPriceBeforeDiscount, result.TotalPriceBeforeDiscount);
        Assert.Equal(totalDiscount, result.TotalDiscount);
        Assert.Equal(finalAmount, result.FinalAmount);
    }


    [Fact]
    public void GenerateInvoice_WithLoyaltyDiscountAndTotalAmountDiscount_ReturnsCorrectInvoice()
    {
        // Arrange
        var products = new List<ProductRequest>
        {
            new ProductRequest { Name = "Product 1", Price = 100, Category = ProductCategory.Other },
            new ProductRequest { Name = "Product 2", Price = 250, Category = ProductCategory.Other },
        };

        var customer = new CustomerRequest { CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _invoiceService.GenerateInvoice(products, customer);

        // Assert
        Assert.Equal(products.Count, result.Products.Count);

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);
        decimal percentageDiscountedAmount = 17.5m; // 5% discount for loyalty
        decimal totalAmountDiscount = 15; // $5 discount for every $100
        decimal totalDiscount = percentageDiscountedAmount + totalAmountDiscount;
        decimal finalAmount = totalPriceBeforeDiscount - totalDiscount;


        Assert.Equal(totalPriceBeforeDiscount, result.TotalPriceBeforeDiscount);
        Assert.Equal(totalDiscount, result.TotalDiscount);
        Assert.Equal(finalAmount, result.FinalAmount);
    }

    [Fact]
    public void GenerateInvoice_WithEmployeeDiscountWithoutGroceryProductsAndTotalAmountDiscount_ReturnsCorrectInvoice()
    {
        // Arrange
        var products = new List<ProductRequest>
        {
            new ProductRequest { Name = "Product 1", Price = 100, Category = ProductCategory.Other },
            new ProductRequest { Name = "Product 2", Price = 50, Category = ProductCategory.Grocery },
        };

        var customer = new CustomerRequest { IsEmployee = true };

        // Act
        var result = _invoiceService.GenerateInvoice(products, customer);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(products.Count, result.Products.Count);

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);
        decimal percentageDiscountedAmount = 30; // 30% discount for employee (doesn't apply to groceries)
        decimal totalAmountDiscount = 5; // $5 discount for every $100
        decimal totalDiscount = percentageDiscountedAmount + totalAmountDiscount;
        decimal finalAmount = totalPriceBeforeDiscount - totalDiscount;

        Assert.Equal(totalPriceBeforeDiscount, result.TotalPriceBeforeDiscount);
        Assert.Equal(totalDiscount, result.TotalDiscount);
        Assert.Equal(finalAmount, result.FinalAmount);
    }

    [Fact]
    public void GenerateInvoice_WithLoyaltyAndAffiliateDiscount_ReturnsCorrectInvoice()
    {
        // Arrange
        var products = new List<ProductRequest>
    {
        new ProductRequest { Name = "Product 1", Price = 160, Category = ProductCategory.Other },
        new ProductRequest { Name = "Product 2", Price = 30, Category = ProductCategory.Other },
    };

        var customer = new CustomerRequest { IsAffiliate = true, CreatedAt = DateTime.Now.AddYears(-2) };

        // Act
        var result = _invoiceService.GenerateInvoice(products, customer);

        // Assert
        Assert.Equal(products.Count, result.Products.Count);

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);
        decimal percentageDiscountedAmount = 19; // 10% discount for affiliate (loyalty discount doesn't apply)
        decimal totalAmountDiscount = 5; // $5 discount for every $100
        decimal totalDiscount = percentageDiscountedAmount + totalAmountDiscount;
        decimal finalAmount = totalPriceBeforeDiscount - totalDiscount;


        // Assuming CalculateDiscountedTotalAmount and GetPercentageDiscountedAmount are correct
        Assert.Equal(totalPriceBeforeDiscount, result.TotalPriceBeforeDiscount);
        Assert.Equal(totalDiscount, result.TotalDiscount); // 10% affiliate + 5% loyalty
        Assert.Equal(finalAmount, result.FinalAmount);
    }

}
