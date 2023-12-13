using ShopsRUs.Contracts.Public.Invoice;
using ShopsRUs.Domain;

namespace ShopsRUs.Application.Interfaces.Services;

public interface IInvoiceService
{
    Invoice GenerateInvoice(List<ProductRequest> products, CustomerRequest customer);
}
