using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubscriptionProvider.Data;
using SubscriptionProvider.Factories;
using SubscriptionProvider.Repositories;
using SubscriptionProvider.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddScoped<SubscriberFactory>();
        services.AddScoped<SubscribeService>();
        services.AddScoped<SubscriberRepository>();

        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<DataContext>(x => x.UseSqlServer(context.Configuration.GetConnectionString("Database")));
    })
    .Build();

host.Run();
