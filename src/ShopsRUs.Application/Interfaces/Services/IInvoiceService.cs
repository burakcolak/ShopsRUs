using ShopsRUs.Domain;

namespace ShopsRUs.Application.Interfaces.Services;

public interface IInvoiceService
{
    Invoice GenerateInvoice(List<Product> products, Customer customer);
}
