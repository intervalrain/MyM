using MyMoney.Api;
using MyMoney.Application;
using MyMoney.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddPresentation()
        .AddInfrastructure(builder.Configuration);

    if (builder.Environment.IsDevelopment())
    {
        builder.Configuration.AddUserSecrets<Program>();
    }
}

var app = builder.Build();
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseInfrastructure();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseAuthorization();
    app.MapSwagger()
        .RequireAuthorization();
    app.MapControllers();

    app.Run();

}