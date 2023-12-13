using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShopsRUs.Application.Discounts;
using ShopsRUs.Application.Interfaces.Services;
using ShopsRUs.Application.Invoices;

namespace ShopsRUs.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var discountSettings = new DiscountSettings();
        configuration.Bind(DiscountSettings.SectionName, discountSettings);
        services.AddSingleton(Options.Create(discountSettings));

        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IDiscountService, DiscountService>();
        return services;

    }
}
