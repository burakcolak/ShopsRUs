using ShopsRUs.Application;
using ShopsRUs.PublicApi;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services
        .AddPresentation()
        .AddApplication();
};

var app = builder.Build();
{
    app.UseDeveloperExceptionPage();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}