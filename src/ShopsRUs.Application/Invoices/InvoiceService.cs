using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Contracts.Public.Invoice;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Invoices;

public class InvoiceService : IInvoiceService
{
    private readonly IDiscountService _discountService;

    public InvoiceService(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public Invoice GenerateInvoice(List<ProductRequest> products, CustomerRequest customerRequest)
    {
        Customer customer = new Customer
        {
            Name = customerRequest.Name,
            IsEmployee = customerRequest.IsEmployee,
            IsAffiliate = customerRequest.IsAffiliate,
            CreatedAt = customerRequest.CreatedAt
        };

        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);

        // Apply percentage discounts to each product
        List<Product> discountedProducts = products.Select(product =>
        {
            decimal discountedPrice = _discountService.GetPercentageDiscountedAmount(new Product
            {
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            }, customer);

            return new Product
            {
                Name = product.Name,
                Price = discountedPrice,
                Category = product.Category
            };
        }).ToList();

        decimal percentageDiscountedTotalAmount = discountedProducts.Sum(p => p.Price);

        // Apply final discount
        decimal finalAmount = _discountService.CalculateDiscountedTotalAmount(percentageDiscountedTotalAmount);

        return new Invoice
        {
            Customer = customer,
            Products = discountedProducts,
            TotalPriceBeforeDiscount = totalPriceBeforeDiscount,
            TotalDiscount = totalPriceBeforeDiscount - finalAmount,
            FinalAmount = finalAmount
        };
    }
}
