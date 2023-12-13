using Mapster;

namespace ShopsRUs.PublicApi.Common.Mapping;

public class InvoiceMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Domain.Invoice, Contracts.Public.Invoice.CreateInvoiceResponse>()
            .Map(dest => dest.Products, src => src.Products)
            .Map(dest => dest.Customer, src => src.Customer)
            .Map(dest => dest.TotalPriceBeforeDiscount, src => src.TotalPriceBeforeDiscount)
            .Map(dest => dest.TotalDiscount, src => src.TotalDiscount)
            .Map(dest => dest.FinalAmount, src => src.FinalAmount);
    }
}
