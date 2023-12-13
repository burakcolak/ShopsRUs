using ShopsRUs.Common.Enums;

namespace ShopsRUs.Contracts.Public.Invoice;
public class CreateInvoiceRequest
{
    public List<ProductRequest> Products { get; set; }
    public CustomerRequest Customer { get; set; }
}

public class ProductRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
}

public class CustomerRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsEmployee { get; set; }
    public bool IsAffiliate { get; set; }
    public DateTime CreatedAt { get; set; }
}
