using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Invoices;

public class InvoiceService : IInvoiceService
{
    private readonly IDiscountService _discountService;

    public InvoiceService(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public Invoice GenerateInvoice(List<Product> products, Customer customer)
    {
        decimal totalPriceBeforeDiscount = products.Sum(p => p.Price);

        // Apply percentage discounts to each product
        List<Product> discountedProducts = products.Select(product =>
        {
            decimal discountedPrice = _discountService.GetPercentageDiscountedAmount(product, customer);
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
