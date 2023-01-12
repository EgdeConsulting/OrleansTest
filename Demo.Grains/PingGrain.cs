using Demo.GrainInterfaces;

namespace Demo.Grains;

public class PingGrain : Grain, IPingGrain
{
    public Task SayHelloWorld()
    {
        Console.WriteLine("yoyoma");
        return Task.CompletedTask;
    }

    public Task<string> ReplyWithSomeThings(int numberOfThings)
    {
        Console.WriteLine($"Get this input: {numberOfThings}");

        if (numberOfThings != 2)
        {
            return Task.FromResult("Det er ikke julebord for noen i morra");
        }
        return Task.FromResult("Det er julebord i morra");
    }
}