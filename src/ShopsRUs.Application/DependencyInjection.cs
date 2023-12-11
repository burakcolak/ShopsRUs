using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Application.Discount;
using ShopsRUs.Application.Interfaces.Services;

namespace ShopsRUs.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IDiscountService, DiscountService>();
        return services;

    }
}
