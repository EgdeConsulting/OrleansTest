using Demo.GrainInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Kan du enable read-only?");

using var host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient((context, builder) =>
    {
        static async Task<bool> ConnectionRetryFilter(Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Cluster client failed to connect to cluster.  Exception: {exception}");

            await Task.Delay(TimeSpan.FromSeconds(4), cancellationToken);
            return true;
        }

        builder.UseLocalhostClustering()
            .UseConnectionRetryFilter(ConnectionRetryFilter);
    })
    .Build();


await host.StartAsync();

var orleansClient = host.Services.GetRequiredService<IClusterClient>();
var pingGrain = orleansClient.GetGrain<IPingGrain>("test-1");
var egdeGrain = orleansClient.GetGrain<IEgdeGrain>("egde");
var lifeTime = host.Services.GetRequiredService<IHostApplicationLifetime>();
while (!lifeTime.ApplicationStopping.IsCancellationRequested)
{
    await Task.Delay(1_000);
    await pingGrain.SayHelloWorld();
    var response = await pingGrain.ReplyWithSomeThings(2);
    await egdeGrain.RegisterName("Øyvind");
    var howManyWithName = await egdeGrain.HowManyWithName("Øyvind");
    Console.WriteLine($"This many with Øyvind: {howManyWithName}");
    Console.WriteLine($"Got this response: {response}");
}

await host.StopAsync();
return 0;