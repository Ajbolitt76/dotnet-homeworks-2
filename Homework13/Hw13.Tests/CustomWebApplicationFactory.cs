using Hw10.DbModels;
using Hw10.Services;
using Hw10.Services.CachedCalculator;
using Hw10.Services.MathCalculator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Hw13.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Hw10.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        MathExpressionTaskBuilder.WaitSecond = false;
        builder.ConfigureServices((context, collection) =>
        {
            collection.AddSingleton<IMathCalculatorService>(s =>
                new MathMemoryCachedCalculatorService(
                    s.GetRequiredService<MathCalculatorService>()));
        });
    }
}