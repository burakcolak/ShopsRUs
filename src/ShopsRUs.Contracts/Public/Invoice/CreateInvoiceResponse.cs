﻿using ShopsRUs.Common.Enums;

namespace ShopsRUs.Contracts.Public.Invoice;
public class CreateInvoiceResponse
{
    public List<ProductResponse> Products { get; set; }
    public CustomerResponse Customer { get; set; }
    public decimal TotalPriceBeforeDiscount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal FinalAmount { get; set; }
}

public class ProductResponse
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
}

public class CustomerResponse
{
    public string Name { get; set; }
}
