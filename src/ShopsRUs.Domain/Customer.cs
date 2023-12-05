namespace ShopsRUs.Domain;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsEmployee { get; set; }
    public bool IsAffiliate { get; set; }
    public DateTime CreatedAt { get; set; }
}
