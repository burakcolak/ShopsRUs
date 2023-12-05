namespace ShopsRUs.Domain;

public class Invoice
{
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalDiscount { get; set; }
}
