using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Discount;

public class InvoiceService : IInvoiceService
{
    public Invoice GenerateInvoice(List<Product> products, Customer customer)
    {
        throw new NotImplementedException();
    }
}
