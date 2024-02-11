using ClassifiedDocumentPortal.BlazorUi.Extensions;
using ClassifiedDocumentPortal.Application.Extensions;
using ClassifiedDocumentPortal.Infrastructure.Extensions;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.SqlServer", LogEventLevel.Information)
    .WriteTo.File("Logs-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddBlazorUi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseSerilogRequestLogging();
app.UseInfrastructure(app.Environment);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();