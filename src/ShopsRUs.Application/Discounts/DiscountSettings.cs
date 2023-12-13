namespace ShopsRUs.Application.Discounts;
public class DiscountSettings
{
    public const string SectionName = "DiscountSettings";
    public decimal EmployeeDiscountPercent { get; set; }
    public decimal AffiliateDiscountPercent { get; set; }
    public decimal CustomerLoyaltyDiscount { get; set; }
    public int DiscountEligibilityYears { get; set; }
    public decimal TotalAmountForDiscount { get; set; }
    public decimal DiscountAmount { get; set; }
}