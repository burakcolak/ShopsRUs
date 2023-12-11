namespace ShopsRUs.Domain;

public class Invoice
{
    public List<Product> Products { get; set; }
    public Customer Customer { get; set; }
    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal FinalAmount { get; set; }
}
