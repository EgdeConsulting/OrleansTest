using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

using var host = Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.UseLocalhostClustering();
        siloBuilder.AddMemoryGrainStorageAsDefault();
    })
    .Build();


await host.StartAsync();
var lifeTime = host.Services.GetRequiredService<IHostApplicationLifetime>();

while (!lifeTime.ApplicationStopping.IsCancellationRequested)
{
    
}

await host.StopAsync();
return 0;