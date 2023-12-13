using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Application.Discounts;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Application.Invoices;

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
